using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_first_iteration
{
    public interface ILoggedInUser
    {
        bool IsLoggedIn { set; get; }
        string FirstName { set; get; }
        string Surname { set; get; }
        string Email { set; get; }
        string Phone { set; get; }
    }
}
