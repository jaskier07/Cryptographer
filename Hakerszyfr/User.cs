using System;
using System.Security.Cryptography;

namespace Cryptographer
{
    public class User
    {
        public String email;
        public String password;
        public String rsaKey;

        public User(String password, String email)
        {
            this.email = email;
            this.password = password;
        }

        public User(String email, String password, String rsaKey)
        {
            this.email = email;
            this.password = password;
            this.rsaKey = rsaKey;
        }
    }
}
