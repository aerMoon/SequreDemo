using System.Security.Cryptography;
using System.Text;

namespace SHA256_Demo
{
    public class Protector
    {
        //словарь для хранения в памяти информации о нескольких пользователях
        private static Dictionary<string, User> Users = new Dictionary<string, User>();
       
        // регистрация нового пользователя
        public static User Register(string username, string password)
        {
            // генерация случайной соли
            var rng = RandomNumberGenerator.Create();
            var saltBytes = new byte[16];
            rng.GetBytes(saltBytes);
            var saltText = Convert.ToBase64String(saltBytes);

            // генерация соленого и хешированного пароля
            var saltedhashedPassword = SaltAndHashPassword(password, saltText);
            
            var user = new User
            {
                Name = username,
                Salt = saltText,
                SaltedHashedPassword = saltedhashedPassword
            };

            Users.Add(user.Name, user);
            return user;
        }
        //проверка пароля
        public static bool CheckPassword(string username, string password)
        {
            if (!Users.ContainsKey(username))
            {
                return false;
            }
            var user = Users[username];
            // повторная генерация соленого и хешированного пароля
            var saltedhashedPassword = SaltAndHashPassword(password, user.Salt);
            return (saltedhashedPassword == user.SaltedHashedPassword);
        }
        //генерация хешированного пароля
        private static string SaltAndHashPassword(string password, string salt)
        {
            var sha = SHA256.Create();
            var saltedPassword = password + salt;
            return Convert.ToBase64String(
            sha.ComputeHash(Encoding.Unicode.GetBytes(saltedPassword)));
        }
    }
}
