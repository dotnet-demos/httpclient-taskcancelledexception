using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System;

namespace HttpClient.Net48.Tests
{
    [TestClass]
    public class HttpClient_GetStringAsync
    {
        [TestMethod]
        async public Task WhenUrlRespondsIn10SecsButHttpClientTimeoutEarlier_ExpectTimeoutException()
        {
            var httpClient = new System.Net.Http.HttpClient
            {
                Timeout = TimeSpan.FromSeconds(2)
            };
            try
            {
                await httpClient.GetStringAsync("https://joymons.free.beeceptor.com/delay"); //Anonymous url responds after 10 seconds delay.
            }
            catch (TaskCanceledException tex)
            {
                if (tex.InnerException is TimeoutException)
                {
                    //Comes here in .Net Core 3.1+ and .Net. Not in .Net Core Below 3.1 and in whole .Net Framework
                    Console.WriteLine($"{tex.GetType()}:{tex.Message}");
                    var timeoutEx = tex.InnerException as TimeoutException;
                    Console.WriteLine($"{timeoutEx.GetType()}:{timeoutEx.Message}");
                }
                else
                {
                    // Comes here in .Net Framework
                    if (tex.CancellationToken.IsCancellationRequested == false)
                    {
                        Console.WriteLine($"{tex.GetType()}:Safely assuming its timeout as IsCancellationRequested = false");
                    }
                    PrintExceptionRecursive(tex);
                }
            }
            catch (AggregateException aex) // This is not hitting .Net Framework & .Net 6
            {
                foreach (var ex in aex.InnerExceptions)
                {
                    PrintExceptionRecursive(ex);
                }
            }
            catch (Exception ex)
            {
                PrintExceptionRecursive(ex);
            }
        }
        private void PrintExceptionRecursive(Exception ex)
        {
            Console.WriteLine($"{ex.GetType()}:{ex.Message}");
            if (ex.InnerException != null) PrintExceptionRecursive(ex.InnerException);
        }
    }
}
