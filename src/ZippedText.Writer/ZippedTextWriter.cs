using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace ZippedText.Writer
{
    public class ZippedTextWriter : IDisposable
    {
        private const string NewLine = "\r\n";
        private byte[] zippedBytes;
        private readonly string fileName;
        public ZippedTextWriter(string fileName)
        {
            this.fileName = fileName;
        }

        public void Dispose()
        {
            zippedBytes = null;
        }

        public byte[] GetZip()
        {
            return zippedBytes;
        }
        public bool Write(IEnumerable<string> lines)
        {
            try
            {
                if (zippedBytes is null || zippedBytes.Length == 0)
                {
                    using (MemoryStream zipStream = new MemoryStream())
                    {
                        using (ZipArchive zipArchive = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
                        {
                            ZipArchiveEntry zipEntry = zipArchive.CreateEntry(fileName);
                            Stream entryStream = zipEntry.Open();
                            foreach (string line in lines)
                            {
                                var bytes = Encoding.UTF8.GetBytes(line + NewLine);
                                entryStream.Write(bytes, 0, bytes.Length);
                            }
                            entryStream.Flush();
                            entryStream.Close();
                        }
                        zipStream.Position = 0;
                        zippedBytes = zipStream.ToArray();
                    }
                }
                else
                {
                    using (MemoryStream tempMemoryStream = new MemoryStream(zippedBytes))
                    {
                        using (MemoryStream zipStream = new MemoryStream())
                        {
                            tempMemoryStream.CopyTo(zipStream);
                            using (ZipArchive zipArchive = new ZipArchive(zipStream, ZipArchiveMode.Update))
                            {
                                ZipArchiveEntry zipEntry = zipArchive.Entries[0];
                                Stream entryStream = zipEntry.Open();
                                entryStream.Position = entryStream.Length - 1;
                                foreach (string line in lines)
                                {
                                    var bytes = Encoding.UTF8.GetBytes(line + NewLine);
                                    entryStream.Write(bytes, 0, bytes.Length);
                                }
                                entryStream.Flush();
                                entryStream.Close();
                            }
                            zippedBytes = zipStream.ToArray();
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Write(string line)
        {
            return Write(new[] { line });
        }
    }
}
