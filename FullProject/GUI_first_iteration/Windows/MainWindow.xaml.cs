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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClientApplication;
using ConsoleApplication1;
using MessageTypes.Messages;
using MessageTypes.ReplyMessages;

namespace GUI_first_iteration
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // -----------------------------------
        // DATA MEMBERS ----------------------
        // -----------------------------------

        private MainMenuWindow mainMenuWin;
        private IClientCmd clientCom;
        private LoginMsg loginObj;
        private bool ClosedInCode;

        // -----------------------------------
        // CONSTRUCTOR - MainWindow ----------
        // -----------------------------------

        public MainWindow(MainMenuWindow parent, IClientCmd ccom)
        {
            mainMenuWin = parent;
            clientCom = ccom;
            loginObj = new LoginMsg();
            ClosedInCode = false;
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        // -----------------------------------
        // BUTTON - New user -----------------
        // -----------------------------------

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreateUserWindow createUserWin = new CreateUserWindow(mainMenuWin, clientCom);
            createUserWin.Show(); // ShowDialog
            ClosedInCode = true;
            this.Close(); 
        }

        // -----------------------------------
        // BUTTON - Login --------------------
        // -----------------------------------

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            // Send object
            var clientCmd = new ClientCmd();
            clientCmd = (ClientCmd) clientCom;
            clientCmd.onLogiMsgReceived += new ClientCmd.LogiDelegate(loginEvent);
            clientCom.SendToServer(loginObj);


        }

        public void loginEvent(ILoginReplyReplyMsg msg)
        {
            if (msg.Email)
            {
                if (msg.Password)
                {
                    mainMenuWin.Show();
                    ClosedInCode = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Wrong password\n Please reenter...");
                }
            }
            else
            {
                MessageBox.Show("Wrong email\n Please reenter...");
            }
        }

        private void tbxPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            tbxOnPSW.Visibility = Visibility.Hidden;
        }

        private void tbxEmail_GotFocus(object sender, RoutedEventArgs e)
        {
            tbxEmail.Text = string.Empty;
        }

        
        private void tbxEmail_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (tbxEmail.Text == string.Empty)
            {
                tbxEmail.Text = "Indtast din mail";
            }
            else 
            { 
                loginObj.Email = tbxEmail.Text;
            }
        }

        private void tbxPassword_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (tbxPassword.Password == string.Empty)
            {
                tbxOnPSW.Text = "Indtast dit password";
                tbxOnPSW.Visibility = Visibility.Visible;
            }
            else
            {
                loginObj.Password = tbxPassword.Password;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
         if (!ClosedInCode)
            {
                Application.Current.Shutdown();
            }

      }

        private void LbExit_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!ClosedInCode)
            {
                Application.Current.Shutdown();
            }

        }

        private void LbMin_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           this.WindowState = WindowState.Minimized;
        }
    }
}
