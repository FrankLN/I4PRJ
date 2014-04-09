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
using ConsoleApplication1;
using DatabaseInterface;
using MessageTypes.Messages;
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

        private CreateUserWindow createUserWin;
        private MainWindow mainWin;
        private ActivationMsg activationMsg;
        private IClientCmd clientCom;
        public string activationCode { get; set; }
        // -----------------------------------
        // CONSTRUCTOR - ActivateUserWindow --
        // -----------------------------------

        public ActivateUserWindow(UserClass user,IClientCmd clientCom, CreateUserWindow uWin, MainWindow mWin)
        {
            InitializeComponent();
            createUserWin = uWin;
            mainWin = mWin;
            activationMsg = new ActivationMsg();
            activationMsg.User = user;
            DataContext = activationMsg.User;
            this.clientCom = clientCom;


            // Center window at startup
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void activateUserEvent(IActivationReplyMsg msg)
        {
            if (msg.UserActivated)
            {
                MessageBox.Show("Good activation code\n You can now log in!");
                mainWin.Show();
                createUserWin.ClosedInCode = true;
                createUserWin.Close();
                this.Close();
            }
        }

        // -----------------------------------
        // BUTTON - Activate user ------------
        // -----------------------------------

        private void btnActivateUser_Click(object sender, RoutedEventArgs e)
        {
            var clientCmd = new ClientCmd();
            clientCmd = (ClientCmd)clientCom;
            clientCmd.onValidateActivationMsgReceived += new ClientCmd.ValidateActivationDelegate(activateUserEvent);
            clientCom.SendToServer(activationMsg);



        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            createUserWin.ClosedInCode = true;
            createUserWin.Close();
        }
    }
}
