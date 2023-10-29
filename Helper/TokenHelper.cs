using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;

namespace ASPNetCoreFLN_APIs.Helper
{
    //public class TokenHelper
    //{
    //}
    public class TokenEncrypterDecrypter
    {
        private static int KEY_SIZE = 192;
        private static int BLOCK_SIZE = 128;

        private static string Key = null;
        private static string IV = null;  //initialization vector       

        public static string Decrypt(string raw, string key)
        {
            TokenEncrypterDecrypter.IV = key.Substring(0, 16);
            string decrypted;
            using (Aes aes = Aes.Create())
            {
                aes.BlockSize = TokenEncrypterDecrypter.BLOCK_SIZE;
                aes.KeySize = TokenEncrypterDecrypter.KEY_SIZE;
                ICryptoTransform decryptor = aes.CreateDecryptor(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(TokenEncrypterDecrypter.IV));
                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(raw)))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sw = new StreamReader(cs))
                        {
                            decrypted = sw.ReadToEnd();
                        }
                    }
                }
            }
            return decrypted;
        }
        public static string Encrypt(string raw, string key)
        {

            TokenEncrypterDecrypter.IV = key.Substring(0, 16);
            byte[] encrypted;
            int rem = raw.Length % 16;  // string length must be multiple of 16..otherwise we have to add extra padding
            if (rem != 0)
            {
                int f = 16 - rem;
                while (f > 0)
                {
                    raw = raw + " ";
                    f--;
                }
            }
            using (Aes aes = Aes.Create())
            {
                aes.BlockSize = TokenEncrypterDecrypter.BLOCK_SIZE;
                aes.KeySize = TokenEncrypterDecrypter.KEY_SIZE;
                ICryptoTransform encryptor = aes.CreateEncryptor(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(TokenEncrypterDecrypter.IV));
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(raw);
                        }
                        encrypted = ms.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(encrypted).ToString();
        }
        public static string CalculateMD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public static bool VerifyMD5Hash(string input, string hash)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return hash == sb.ToString();
            }
        }
    }
}
