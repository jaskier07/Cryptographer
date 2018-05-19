using System;

namespace Cryptographer
{
    public class User
    {
        public String email;
        public String password;
        public String publicKey;
        public String privateKey;

        public User(String password, String email)
        {
            this.email = email;
            this.password = password;
        }

        public User(String email, String password, String publicKey, String privateKey)
        {
            this.email = email;
            this.password = password;
            this.publicKey = publicKey;
            this.privateKey = privateKey;
        }
    }
}
