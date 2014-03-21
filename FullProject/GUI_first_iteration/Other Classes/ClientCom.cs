using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUI_first_iteration
{
    class ClientCom : IClientCom
    {
        public void RequestLogin( string email, string password)
        {
            // Create new background thread
            /* - Send request with needed information
             * - Wait for reply
             * - Forward reply to CmdHandlerC
             */

            MessageBox.Show("ClientCom >> RequestLogin() called with parameters: " + email + " " + password);

            // cmdHandler.Give("LOGIN_REQ", "Wrong email / wrong password / ALL OK");
        }

        public void CreateNewUser(string details, int lenght)
        {
            MessageBox.Show("ClientCom >> CreateNewUser() called with string: " + details + "   and lenght: " + lenght);
        }

        public void CreateNewJob(string details, int lenght)
        {
            MessageBox.Show("ClientCom >> CreateNewJob() called with string: " + details + "   and lenght: " + lenght);
        }
    }

}
