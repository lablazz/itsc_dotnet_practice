using System.Security.Cryptography;
using System.Text;

namespace itsc_dotnet_practice.Helpers;

public static class EncryptionHelper
{
    private static readonly string Key = "YourSuperSecretKey123"; // store securely (use env or config)
    private static readonly string IV = "YourInitVector1234";     // 16 chars (128-bit)

    public static string Encrypt(string plainText)
    {
        using var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(Key.PadRight(32)); // 256-bit key
        aes.IV = Encoding.UTF8.GetBytes(IV);

        using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        using var ms = new MemoryStream();
        using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
        using (var sw = new StreamWriter(cs))
        {
            sw.Write(plainText);
        }

        return Convert.ToBase64String(ms.ToArray());
    }

    public static string Decrypt(string encryptedText)
    {
        using var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(Key.PadRight(32));
        aes.IV = Encoding.UTF8.GetBytes(IV);

        using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        using var ms = new MemoryStream(Convert.FromBase64String(encryptedText));
        using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
        using var sr = new StreamReader(cs);
        return sr.ReadToEnd();
    }
}
