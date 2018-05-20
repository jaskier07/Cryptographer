using System;
using System.Collections.Generic;

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
                usersList.Add(new User(userData[0], userData[1], userData[2])); // email, password, rsaKey
            }
        }
    }
}