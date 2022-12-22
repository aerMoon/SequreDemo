using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AES_Demo
{
    public static class Protector
    {
        private static readonly byte[] salt =Encoding.Unicode.GetBytes("someText!");
        private static readonly int iterations = 1000;
        
        
        public static string Encrypt(string plainText, string password)
        {
            byte[] encryptedBytes;
            byte[] plainBytes = Encoding.Unicode.GetBytes(plainText);
            var aes = Aes.Create(); // генерирующий метод абстрактных классов
            
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);

            aes.Key = pbkdf2.GetBytes(32); // установить 256-битный ключ
            aes.IV = pbkdf2.GetBytes(16); // установить 128-битный вектор инициализации
        
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, aes.CreateEncryptor(),CryptoStreamMode.Write))
                {
                    cs.Write(plainBytes, 0, plainBytes.Length);
                }
                encryptedBytes = ms.ToArray();
            }
            return Convert.ToBase64String(encryptedBytes);
        }
        public static string Decrypt(string cryptoText, string password)
        {
            byte[] plainBytes;
            byte[] cryptoBytes = Convert.FromBase64String(cryptoText);
            var aes = Aes.Create();

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);

            aes.Key = pbkdf2.GetBytes(32);
            aes.IV = pbkdf2.GetBytes(16);
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cryptoBytes, 0, cryptoBytes.Length);
                }
                plainBytes = ms.ToArray();
            }
            return Encoding.Unicode.GetString(plainBytes);
        }
        }
}
