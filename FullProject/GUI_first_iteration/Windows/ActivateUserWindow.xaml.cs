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
using MessageTypes.ReplyMessages;

namespace GUI_first_iteration
{
    /// <summary>
    /// Interaction logic for ActivateUserWindow.xaml
    /// </summary>
    public partial class ActivateUserWindow : Window
    {
        // -----------------------------------
        // DATA MEMBERS ----------------------  
        // -----------------------------------

        private ICreateUserReplyMsg userReplyMsg;
        private CreateUserWindow createUserWin;
        private MainWindow mainWin;
        private ActivateUserCom activateUserObj;

        // -----------------------------------
        // CONSTRUCTOR - ActivateUserWindow --
        // -----------------------------------

        public ActivateUserWindow(ICreateUserReplyMsg msg, CreateUserWindow uWin, MainWindow mWin)
        {
            userReplyMsg = msg;
            createUserWin = uWin;
            mainWin = mWin;
            activateUserObj = new ActivateUserCom();
            InitializeComponent();

            // Center window at startup
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        // -----------------------------------
        // BUTTON - Activate user ------------
        // -----------------------------------

        private void btnActivateUser_Click(object sender, RoutedEventArgs e)
        {
            activateUserObj.ActivationKey = tbxActivateUser.Text;
            if (activateUserObj.ActivationKey == userReplyMsg.ActivationCode)
            {
                MessageBox.Show("Good activation code\n You can now log in!");
                mainWin.Show();
                createUserWin.Close();
                this.Close();
               
            }


        }
    }
}
