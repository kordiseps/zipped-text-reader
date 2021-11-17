using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace ZippedText.Reader
{
    public class ZippedTextReader
    {
        private readonly byte[] zippedData;
        private readonly string fileName = string.Empty;
        public ZippedTextReader(byte[] zippedData)
        {
            this.zippedData = zippedData;
        }
        public ZippedTextReader(byte[] zippedData, string fileName)
        {
            this.zippedData = zippedData;
            this.fileName = fileName;
        }

        private List<string> bufferedLines = new List<string>();
        private long index = 0;
        private bool ended = false;
        private int defaultLineCount = 10_000;


        public bool Read()
        {
            return Read(defaultLineCount);
        }
        public bool Read(int lineCount)
        {
            if (ended)
            {
                return false;
            }

            bufferedLines = new List<string>();
            using (MemoryStream memoryStream = new MemoryStream(zippedData))
            {
                using (ZipArchive zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Read))
                {

                    if (zipArchive.Entries.Count == 0)
                    {
                        throw new Exception("Zip File does not include a file");
                    }

                    ZipArchiveEntry zipArchiveEntry;
                    if (!string.IsNullOrWhiteSpace(fileName))
                    {
                        try
                        {
                            zipArchiveEntry = zipArchive.GetEntry(fileName);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception($"'{fileName}' could not read from Zip File.", ex);
                        }
                    }
                    else if (zipArchive.Entries.Count > 1)
                    {
                        throw new Exception("Zip File does include more than one file. Specify file name.");
                    }
                    else
                    {
                        zipArchiveEntry = zipArchive.Entries[0];
                    }

                    using (Stream stream = zipArchiveEntry.Open())
                    {
                        using (BufferedStream bs = new BufferedStream(stream))
                        {
                            using (StreamReader sr = new StreamReader(bs))
                            {
                                long internalIndex = 0;
                                string line;
                                while ((line = sr.ReadLine()) != null)
                                {
                                    if (internalIndex >= index)
                                    {
                                        if (bufferedLines.Count < lineCount)
                                        {
                                            bufferedLines.Add(line);
                                            index++;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    internalIndex++;
                                }
                                if (bufferedLines.Count < lineCount)
                                {
                                    ended = true;
                                }

                                return true;
                            }
                        }
                    }
                }
            }
        }

        private List<string> GetBufferedLines()
        {
            return bufferedLines;
        }

        public static implicit operator List<string>(ZippedTextReader reader)
        {
            return reader.GetBufferedLines();
        }
    }
}
