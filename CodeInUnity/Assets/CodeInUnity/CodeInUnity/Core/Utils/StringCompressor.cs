using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace CodeInUnity.Core.Utils
{
  public static class StringCompressor
  {
    public static string Compress(string text)
    {
      byte[] buffer = Encoding.UTF8.GetBytes(text);

      using (var ms = new MemoryStream())
      {
        using (var zip = new GZipStream(ms, CompressionMode.Compress))
        {
          zip.Write(buffer, 0, buffer.Length);
        }

        return Convert.ToBase64String(ms.ToArray());
      }
    }

    public static string Decompress(string compressedText)
    {
      byte[] gZipBuffer = Convert.FromBase64String(compressedText);

      using (var ms = new MemoryStream(gZipBuffer))
      using (var zip = new GZipStream(ms, CompressionMode.Decompress))
      using (var result = new MemoryStream())
      {
        zip.CopyTo(result);
        return Encoding.UTF8.GetString(result.ToArray());
      }
    }
  }
}
