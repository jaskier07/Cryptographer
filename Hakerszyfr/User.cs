using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptographer
{
    public class User
    { 
        public String password;
        public String email;

        public User(String password, String email)
        {
            this.password = password;
            this.email = email;
        }

        public User()
        {
        }
    }
}
