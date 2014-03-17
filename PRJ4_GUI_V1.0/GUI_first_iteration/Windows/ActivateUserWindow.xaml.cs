using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI_first_iteration
{
    /// <summary>
    /// Interaction logic for ActivateUserWindow.xaml
    /// </summary>
    public partial class ActivateUserWindow : Window
    {
        private CreateUserWindow main_parent;
        private ActivateUserCom activateUserObj;

        public ActivateUserWindow(CreateUserWindow parent)
        {
            main_parent = parent;
            activateUserObj = new ActivateUserCom();
            InitializeComponent();

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void btnActivateUser_Click(object sender, RoutedEventArgs e)
        {
            activateUserObj.ActivationKey = tbxActivateUser.Text;
            activateUserObj.Print();
        }

    }
}
