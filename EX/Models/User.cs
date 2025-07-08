using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EX.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; } = true;

        public static List<User> usersList = new List<User>();

        // יצירת סיסמה עם salt והמרה לשמירה בטוחה
        public static string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16); // 128 ביט
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(32); // 256 ביט
                byte[] hashBytes = new byte[48];
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 32);
                return Convert.ToBase64String(hashBytes);
            }
        }

        // בדיקת סיסמה בזמן Login
        public static bool VerifyPassword(string enteredPassword, string storedHash)
        {
            byte[] hashBytes = Convert.FromBase64String(storedHash);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            using (var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, 100_000, HashAlgorithmName.SHA256))
            {
                byte[] enteredHash = pbkdf2.GetBytes(32);
                for (int i = 0; i < 32; i++)
                {
                    if (hashBytes[i + 16] != enteredHash[i])
                        return false;
                }
            }
            return true;
        }

        public static bool Register(User u)
        {
            if (usersList.Any(x => x.Email == u.Email))
                return false;

            u.Password = HashPassword(u.Password);
            usersList.Add(u);
            return true;
        }

        public static bool Login(string email, string password)
        {
            var user = usersList.FirstOrDefault(u => u.Email == email);
            if (user == null)
                return false;

            return VerifyPassword(password, user.Password);
        }

        public static List<User> Read()
        {
            return usersList;
        }
    }
}

