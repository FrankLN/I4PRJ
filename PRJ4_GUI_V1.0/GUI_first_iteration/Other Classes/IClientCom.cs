using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_first_iteration
{
    public interface IClientCom
    {
        void RequestLogin(string email, string password);

        void CreateNewUser(string details, int lenght);

        void CreateNewJob(string details, int lenght);
    }
}
