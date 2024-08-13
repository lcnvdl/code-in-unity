using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace CodeInUnity.Core.Utils
{
  public static class HashUtils
  {
    public static string GetMd5Hash(string str)
    {
      return GetHash(str, new MD5CryptoServiceProvider());
    }

    public static string GetSha1Hash(string str)
    {
      return GetHash(str, new SHA1Managed());
    }

    public static string GetSha256Hash(string str)
    {
      return GetHash(str, new SHA256Managed());
    }

    private static string GetHash(string str, HashAlgorithm hash)
    {
      try
      {
        StringBuilder sb = new StringBuilder();

        foreach (byte b in hash.ComputeHash(ASCIIEncoding.ASCII.GetBytes(str)))
        {
          sb.Append(b.ToString("X2"));
        }

        return sb.ToString().Replace(" ", "");
      }
      catch (Exception ex)
      {
        Debug.LogException(ex);
        return "";
      }
    }
  }
}
