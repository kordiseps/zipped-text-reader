# Zipped Text Reader
Simple, low memory consuming zipped text file reader for .Net.  
This library aims to consume less RAM by reading a certain part of the zipped file without extracting the entire file.  
Usage for this package is inspired by the [SqlDataReader](https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqldatareader) package.  
By saying `reader.Read()`, a certain number of lines are read from the zipped text file each time.  
Thus, no need to extract whole file to read.

## Installation  

- Package Manager : `Install-Package ZippedText.Reader -Version 1.0.0`
- .NET CLI : `dotnet add package ZippedText.Reader --version 1.0.0`
- PackageReference : `<PackageReference Include="ZippedText.Reader" Version="1.0.0" />`  

## Dependencies  

    .NETCoreApp 3.1
        No dependencies.
    .NETStandard 2.0
        No dependencies.
    net5.0
        No dependencies.
    net6.0
        No dependencies.

## Usage
Usage for this package is inspired by the [SqlDataReader](https://docs.microsoft.com/tr-tr/dotnet/api/system.data.sqlclient.sqldatareader?view=dotnet-plat-ext-6.0) package.  
Need to add `using ZippedText.Reader;` statement to beginning of the .cs file.  

```csharp
byte[] zippedTextFileBytes = File.ReadAllBytes("zipped-text-file-path");
ZippedTextReader reader = new ZippedTextReader(zippedTextFileBytes);
while (reader.Read())
{
    List<string> lines = (List<string>)reader;
    // or simply : 
    //List<string> lines = reader;

    // Do stuff with lines
}
```  

`ZippedTextReader` has implicity to `List<string>`. That means, after `Read` method the read lines can be accesible by this way:  

```csharp
List<string> lines = (List<string>)reader;
// or simply : 
//List<string> lines = reader;
```  

The `Read` method reads 10.000 lines by default. This default value choosen because of ram-consuming and speed balance.  
For other cases line count to read can be changeable by specifying count as parameter for `Read` method.  

```csharp
int count = 100_000;
//or 
//int count = 10;
while (reader.Read(count))
{
...
```  

`ZippedTextReader` assumes there is one file in the zip file. If zip file has more than one file, file name needs to be specified in constructor.  

```csharp
byte[] zippedTextFileBytes = File.ReadAllBytes("zipped-text-file-path");
ZippedTextReader reader = new ZippedTextReader(zippedTextFileBytes,"certain-text-file-name");

```  

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
