using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Cryptographer
{
    public class UsersControler
    {
        public static List<User> usersList = new List<User>();
        public static User currentUser = null;

        public UsersControler()
        {
            string[] usersCredentials = System.IO.File.ReadAllLines(@"Users login details.txt");

            foreach (string line in usersCredentials)
            {
                String[] userData = line.Split('|');
                usersList.Add(new User(userData[0], userData[1], userData[2], userData[3]));
            }


        }

        public static string GenerateSHA512String(string inputString)
        {
            SHA512 sha512 = SHA512.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha512.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }
    }
}