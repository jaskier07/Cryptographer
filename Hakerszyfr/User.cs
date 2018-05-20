using System;

namespace Cryptographer
{
    public class User
    {
        public String email;
        public String password;
        public String rsaPublicKey; // newRsaKeys.ToXmlString(false)
        public String rsaPublicPrivateKey; //// newRsaKeys.ToXmlString(true)

        public User(String password, String email)
        {
            this.email = email;
            this.password = password;
        }

        public User(String email, String password, String rsaPublicKey, String rsaPublicPrivateKey)
        {
            this.email = email;
            this.password = password;
            this.rsaPublicKey = rsaPublicKey;
            this.rsaPublicPrivateKey = rsaPublicPrivateKey;
        }
    }
}
