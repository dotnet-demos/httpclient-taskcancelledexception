# Demos difference between HttpClient Timeout in .Net Framework and .Net 6

# How to Run

There are 2 test projects in the solution. One is .Net Framework 4.8 and other .Net 6. 
Run the tests. 

Prefer Visual Studio 2022 and run in debug mode.

## Output .Net 4.8
![.Net 4.8 showing no inner TimeoutExeption](/images/01-net48.png)

## Output .Net 6
![.Net 6 showing inner TimeoutExeption](/images/02-net6.png)

# Specifications

- .Net version - .Net Framework 4.8 & .Net 6
- Nugets referenced
	- coverlet.collector