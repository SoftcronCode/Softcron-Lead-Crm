using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Web;

namespace DSERP_Client_UI
{
    public class compress
    {
        // Method to decompress gzip response
        public string Unzip(string compressedText)
        {
            byte[] compressedBytes = Convert.FromBase64String(compressedText);

            using (var compressedStream = new MemoryStream(compressedBytes))
            using (var decompressedStream = new MemoryStream())
            {
                using (var gzipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
                {
                    gzipStream.CopyTo(decompressedStream);
                }

                decompressedStream.Position = 0;
                using (var reader = new StreamReader(decompressedStream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}