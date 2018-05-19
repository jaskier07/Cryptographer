using System;

namespace Cryptographer
{
    public class User
    {
        public String login;
        public String password;
        public String email;
        public String publicKey;
        public String privateKey;

        public User(String password, String email)
        {
            this.password = password;
            this.email = email;
        }
        public User(String login, String password, String email, String publicKey, String privateKey)
        {
            this.login = login;
            this.password = password;
            this.email = email;
            this.publicKey = publicKey;
            this.privateKey = privateKey;
        }
    }
}
