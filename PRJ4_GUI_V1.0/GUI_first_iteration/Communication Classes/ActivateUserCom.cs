using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUI_first_iteration
{
    class ActivateUserCom
    {
        public string ActivationKey { set; get; }

        public void Print()
        {
            MessageBox.Show("[upLink:ActivateUserCom]" + this.ActivationKey);
        }
    }
}
