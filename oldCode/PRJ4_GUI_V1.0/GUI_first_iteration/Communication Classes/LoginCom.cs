using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUI_first_iteration
{
    class LoginCom
    {
        public string Email { set; get; }
        public string Password { set; get; }

        public void Print()
        {
            MessageBox.Show("[upLink:LoginCom]" + this.Email + this.Password);
        }
    }
}
