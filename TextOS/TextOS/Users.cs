using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextOS
{
    public class Users
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public Users(string Username, string Password)
        {
            this.Username = Username;
            this.Password = Password;
        }
    }
}
