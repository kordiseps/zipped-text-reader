# Zipped Text Reader and Writer

This repository contains zipped file reader and writer libraries for .Net for better memory usage purposes.

## Requirements

- [.Net SDK](https://dotnet.microsoft.com/en-us/download/dotnet)

## Usage

### Repository Usage

- Clone this repository to local
- To build .sln file, use `dotnet build {.sln directory}` command
> Eg: `dotnet build src`
- To build .csproj file, use `dotnet build {.csproj directory}` command
> Eg: `dotnet build src/ZippedText.Reader`
- To create nuget package (nupkg), use `dotnet pack {.csproj directory} -p:PackageVersion={version}` command
> Eg: `dotnet pack src/ZippedText.Reader -p:PackageVersion=1.1.2`
<!-- - To run tests, use `dotnet test ./messaging-service/MessagingService.Api.Test/MessagingService.Api.Test.csproj` -->

### Library Usage

- [Zipped Text Reader](/src/ZippedText.Reader/README.md)
- [Zipped Text Writer](/src/ZippedText.Writer/README.md)

## Licence

MIT License

Copyright (c) 2021 Said Yeter

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
