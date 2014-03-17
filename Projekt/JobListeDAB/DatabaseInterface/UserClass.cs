using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseInterface
{
    public class UserClass
    {
        public UserClass()
        {
            Email = null;
            Name = null;
            PhoneNumber = null;
            Password = null;
            AdminRights = 0;
        }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public int AdminRights { get; set; }
    }
}
