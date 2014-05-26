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
using ClientApplication;
using DatabaseInterface;
using MessageTypes;
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

        /// <summary>
        /// Constructor for ActivateUserWindow. Referencer til instanserne af de pågældende parametre gemmes som private datamembers. 
        /// Derudover oprettes en instans af klassen activationMsgMsg, der skal sendes til server. Datacontexten sættes til at pege på User property af klassen activationMsg.
        /// </summary>
        /// <param name="user"> Reference til instansen af klassen UserClass, der repræsenterer den oprettede bruger</param>
        /// <param name="clientCom">Reference til instansen af klassen ClienCmd, der står for kommunikation til serveren</param>
        /// <param name="uWin">Reference til instansen af CreateUserWindiw, som ActivationUserWindow oprettes fra</param>
        /// <param name="mWin">Reference til instansen af MainWindow, som oprettes efter aktivering</param>
        public ActivateUserWindow(UserClass user,IClientCmd clientCom, CreateUserWindow uWin, MainWindow mWin)
        {
            InitializeComponent();
            createUserWin = uWin;
            mainWin = mWin;
            activationMsg = new ActivationMsg();
            activationMsg.User = user;
            DataContext = activationMsg.User;
            this.clientCom = clientCom;

            ((ClientCmd)clientCom).onValidateActivationMsgReceived += new ClientCmd.ValidateActivationDelegate(activateUserEvent);

            // Center window at startup
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        // -----------------------------------------------------------
        // Event - Besked til Bruger om han er aktiveret -----------------------
        // -----------------------------------------------------------

        /// <summary>
        /// Event der kaldes når serveren svarer på GUIs request om at Bruger er aktiveret. 
        /// Bruger får besked om han er aktiveret eller nej.
        /// </summary>
        /// <param name="msg"> Besked modtaget fra serveren</param>
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
            else
            {
                MessageBox.Show("Could not confirm activation code. Please try again.");
            }
        }

        // -----------------------------------
        // BUTTON - Activate user ------------
        // -----------------------------------

        /// <summary>
        /// Funktion der kaldes ved tryk på knap for at aktivere Bruger. Det som er indtastet af bruger bliver sendt til server.
        /// </summary>
        /// <param name="sender">Indeholder information om hvor funktionen kaldes fra.</param>
        /// <param name="e">Indeholder information om eventet der sætter i gang funktionen.</param>
        private void btnActivateUser_Click(object sender, RoutedEventArgs e)
        {
            clientCom.SendToServer(activationMsg);
        }

        // -----------------------------------
        // METHOD - Window closing -----------
        // -----------------------------------

        /// <summary>
        /// Funktion der kaldes når vinduet lukkes. 
        /// </summary>
        /// <param name="sender">Indeholder information om hvor funktionen kaldes fra.</param>
        /// <param name="e">Indeholder information om eventet der sætter i gang funktionen.</param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ((ClientCmd)clientCom).onValidateActivationMsgReceived -= new ClientCmd.ValidateActivationDelegate(activateUserEvent);

            mainWin.Show();

            createUserWin.Close();
        }
    }
}
