using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace CodeInUnity.Core.Utils
{
    public static class HashUtils
    {
        public static String GetMd5Hash(String str)
        {
            return GetHash(str, new MD5CryptoServiceProvider());
        }

        public static String GetSha1Hash(String str)
        {
            return GetHash(str, new SHA1Managed());
        }

        public static String GetSha256Hash(String str)
        {
            return GetHash(str, new SHA256Managed());
        }

        private static String GetHash(String str, HashAlgorithm hash)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                foreach (Byte b in hash.ComputeHash(ASCIIEncoding.ASCII.GetBytes(str)))
                {
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString().Replace(" ","");
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                return "";
            }
        }
    }
}
