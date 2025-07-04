using System.Security.Cryptography;
using System.Text;

namespace itsc_dotnet_practice.Utilities
{
    public static class EncryptionUtility
    {
        private static readonly byte[] AesKey;
        private static readonly byte[] AesIV;

        static EncryptionUtility()
        {
            string keyString = Environment.GetEnvironmentVariable("AES_KEY");
            string ivString = Environment.GetEnvironmentVariable("AES_IV");

            if (string.IsNullOrEmpty(keyString))
                throw new Exception("AES_KEY environment variable is missing.");

            if (string.IsNullOrEmpty(ivString))
                throw new Exception("AES_IV environment variable is missing.");

            AesKey = Encoding.UTF8.GetBytes(keyString);
            AesIV = Encoding.UTF8.GetBytes(ivString);

            if (AesKey.Length != 32)
                throw new Exception("AES_KEY must be exactly 32 bytes long.");

            if (AesIV.Length != 16)
                throw new Exception("AES_IV must be exactly 16 bytes long.");
        }

        public static string EncryptString(string plainText)
        {
            using var aes = Aes.Create();
            aes.Key = AesKey;
            aes.IV = AesIV;

            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(plainText);
            }

            return Convert.ToBase64String(ms.ToArray());
        }

        public static string DecryptString(string cipherText)
        {
            using var aes = Aes.Create();
            aes.Key = AesKey;
            aes.IV = AesIV;

            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            var buffer = Convert.FromBase64String(cipherText);
            using var ms = new MemoryStream(buffer);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);

            return sr.ReadToEnd();
        }
    }
}