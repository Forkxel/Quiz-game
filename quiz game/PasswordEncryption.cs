using System.Security.Cryptography;
using System.Text;

namespace quiz_game;

public class PasswordEncryption
{
    private static byte[] key = Encoding.UTF8.GetBytes("1234567890123456");
    private static byte[] iv = Encoding.UTF8.GetBytes("1234567890123456");

    public static string Encrypt(string password)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;
            
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                    {
                        streamWriter.Write(password);   
                        streamWriter.Close();
                        return Convert.ToBase64String(memoryStream.ToArray());
                    }  
                }
            }
        }
    }

    public static string Decrypt(string password)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;
            
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(password)))
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader streamReader = new StreamReader(cryptoStream))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }
    }
}