using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_first_iteration
{
    class LoggedInUser : ILoggedInUser
    {
        public bool IsLoggedIn { set; get; }
        public string FirstName { set; get; }
        public string Surname { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
    }
}
