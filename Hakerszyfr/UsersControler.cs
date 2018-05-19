using System;
using System.Collections.Generic;

namespace Cryptographer
{
    public class UsersControler
    {
        List<User> usersList = new List<User>();

        public UsersControler()
        {
            string[] usersCredentials = System.IO.File.ReadAllLines(@"Users login details.txt");

            foreach (string line in usersCredentials)
            {
                String[] userData = line.Split('|');
                usersList.Add(new User(userData[0], userData[1], userData[2], userData[3], userData[4])); //login,password,email,publicKey,privateKey    
            }
        }
    }
}