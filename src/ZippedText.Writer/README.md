# Zipped Text Writer
Simple, low memory consuming zipped text writer for .Net.  
This library aims to consume less RAM by writing a certain part of the text file without holding the entire file on the memory. So that means you don't need to prepare entire file to compress.
Usage for this package is inspired by the [StringBuilder](https://docs.microsoft.com/en-us/dotnet/api/system.text.stringbuilder) package.  
By saying `writer.Write(stringVariable)`, string variable value as a line are written into zip file. Also `Write` method has overload for string collection. You can pass any collection of string that implements to `IEnumerable<string>`.

<!-- ## Installation  

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
        No dependencies. -->

## Usage  
Usage for this package is inspired by the [StringBuilder](https://docs.microsoft.com/en-us/dotnet/api/system.text.stringbuilder) package.    
Need to add `using ZippedText.Writer;` statement to beginning of the .cs file.  

```csharp
ZippedTextWriter writer = new ZippedTextWriter("some.xml");
writer.Write($"<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
writer.Write($"<somexmlroot>");
writer.Write($"\t<somexmlelement1>some content 1</somexmlelement1>");
writer.Write($"\t<somexmlelement2>some content 2</somexmlelement2>");
writer.Write($"</somexmlroot>");
File.WriteAllBytes("some.zip", writer.GetZip());
```  

The `Write` method has overload for `IEnumerable<string>`. With this way, opening compressed file process happens once for all lines instead of per line. Suggested way is this.  
```csharp
ZippedTextWriter writer = new ZippedTextWriter("somefile.txt");

string[] lines = new[]
{
    "line 1",
    "line 2",
    "line 3",
};
writer.Write(lines); 
// or
IQueryable<string> lines = dbContext.TableName
    .Where(x => x.Year > 1992)
    .Select(x => x.Description);
writer.Write(lines);
```  

The constructor expects a string for file name. `GetZip` method returns a compressed file as bytes, that compressed file contains single file named by a file name that specified in constructor.

```csharp
ZippedTextWriter writer = new ZippedTextWriter("somefile.txt");
writer.Write($"some content");
File.WriteAllBytes("some.zip", writer.GetZip()); 
// that 'some.zip' file contains 'somefile.txt' file. 
// that 'somefile.txt' file contains 'some content'.
```  

`ZippedTextWriter` has implementation to `IDisposable`.  

```csharp
using (ZippedTextWriter writer = new ZippedTextWriter("some.xml"))
{
    writer.Write($"some content");
    File.WriteAllBytes("some.zip", writer.GetZip());
}
```  
or in a modern way

```csharp
using ZippedTextWriter writer = new ZippedTextWriter("some.xml");
writer.Write($"some content");
File.WriteAllBytes("some.zip", writer.GetZip());

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
