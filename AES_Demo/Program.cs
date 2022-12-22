namespace AES_Demo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // пример реализации симметричного шифрование на основе протокола AES
            // основной функционал в классе Protector
             
            Console.WriteLine("введите строку для шифрования");
            string text = Console.ReadLine();
            Console.WriteLine("введите пароль:");
            string pass = Console.ReadLine();

            string encryptedText = Protector.Encrypt(text, pass);
            Console.WriteLine($"\nзашифрованный текст:\n{encryptedText}");

            Console.WriteLine("для расшифровки введите пароль:");
            string pass2 = Console.ReadLine();  
            
            string decryptedText = Protector.Decrypt(encryptedText, pass2);
            Console.WriteLine($"\nдешифрованный текст:\n{decryptedText}");


        }
    }
}   