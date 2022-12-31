namespace CodeInUnity.Core.Security
{
    public interface IEncryptor
    {
        void SetKey(object key);
        byte[] Encrypt(byte[] bytes);
    }

    public interface IDecryptor
    {
        void SetKey(object key);
        byte[] Decrypt(byte[] bytes);
    }

    public interface IEnDecryptor
    {
        void SetKey(object key);
        byte[] EnDecrypt(byte[] bytes);
    }
}
