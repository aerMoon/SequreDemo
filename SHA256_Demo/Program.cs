namespace SHA256_Demo
{
    class Program
    {
        //демонстрация использования хешей для безопасного хранения учетных данных пользователя 
        static void Main()
        {
            Console.WriteLine("Регистрация пользователя с именем \"root\" с паролем \'Pa$$w0rd\"");
            User root = Protector.Register("root", "Pa$$w0rd");
            Console.WriteLine($"Имя: {root.Name}" +
                $"\nСоль: {root.Salt}" +
                $"Пароль (соленый и хешированный): {root.SaltedHashedPassword} \n");

            Console.Write("введите нового пользователя для регистрации: ");
            string username = Console.ReadLine();
            Console.Write($"введите пароль для {username}: ");
            string password = Console.ReadLine();
            User user = Protector.Register(username, password);

            Console.WriteLine($"Имя: {user.Name}\n" +
                $"Соль: {user.Salt}\n" +
                $"Пароль (соленый и хешированный): {user.SaltedHashedPassword} \n");
          
            bool correctPassword = false;
            while (!correctPassword)
            {
                Console.Write("введите имя пользователя для авторизации: ");
                string loginUsername = Console.ReadLine();
                Console.Write("введите пароль для атворизации: ");
                string loginPassword = Console.ReadLine();
                
                correctPassword = Protector.CheckPassword(loginUsername, loginPassword);
                if (correctPassword)
                {
                    Console.WriteLine($"Успешный входа для пользователя {loginUsername}");
                }
                else
                {
                    Console.WriteLine("Неверный логин или пароль. повторите ввод");
                }
            }
        }
    }

}

    