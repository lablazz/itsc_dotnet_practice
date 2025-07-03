using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;

namespace itsc_dotnet_practice.Helpers
{
    public static class EncryptionHelper
    {
        // Replace these with your actual secure keys, stored securely in config/env variables
        private static readonly byte[] AesKey = Encoding.UTF8.GetBytes("your-32-byte-long-encryption-key-here!"); // 32 bytes for AES-256
        private static readonly byte[] AesIV = Encoding.UTF8.GetBytes("your-16-byte-iv123"); // 16 bytes for AES

        public static string EncryptString(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return plainText;

            using Aes aes = Aes.Create();
            aes.Key = AesKey;
            aes.IV = AesIV;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using MemoryStream ms = new();
            using CryptoStream cs = new(ms, encryptor, CryptoStreamMode.Write);
            using (StreamWriter sw = new(cs))
            {
                sw.Write(plainText);
            }

            return Convert.ToBase64String(ms.ToArray());
        }

        public static string DecryptString(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
                return cipherText;

            using Aes aes = Aes.Create();
            aes.Key = AesKey;
            aes.IV = AesIV;

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using MemoryStream ms = new(Convert.FromBase64String(cipherText));
            using CryptoStream cs = new(ms, decryptor, CryptoStreamMode.Read);
            using StreamReader sr = new(cs);

            return sr.ReadToEnd();
        }

        // Hash password with BCrypt
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Password cannot be empty");

            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        // Verify hashed password with BCrypt
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hashedPassword))
                return false;

            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
