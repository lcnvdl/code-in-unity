using System;
using System.Text;

namespace CodeInUnity.Core.Security
{
    public class XorEncryptor : IEnDecryptor
    {
        byte[] key;

        public XorEncryptor(string key)
        {
            SetKey(GetBytes(key));
        }

        public void SetKey(object key)
        {
            this.key = (byte[])key;
        }

        public string EnDecrypt(string str)
        {
            return GetString(EnDecrypt(GetBytes(str)));
        }

        public byte[] EnDecrypt(byte[] bytes)
        {
            byte[] output = new byte[bytes.Length];

            for (long i = 0; i < bytes.LongLength; i++)
            {
                output[i] = (byte)(bytes[i] ^ key[i % key.Length]);
            }

            return output;
        }

        private static byte[] GetBytes(string str)
        {
            //byte[] bytes = new byte[str.Length * sizeof(char)];
            //System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            //return bytes;

            throw new NotImplementedException();
        }

        private static string GetString(byte[] bytes)
        {
            //char[] chars = new char[bytes.Length / sizeof(char)];
            //System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            //return new string(chars);

            throw new NotImplementedException();
        }
    }
}
