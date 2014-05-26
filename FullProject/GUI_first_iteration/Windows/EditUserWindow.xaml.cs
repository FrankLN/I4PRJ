using System.Windows;
using DatabaseInterface;
using MessageTypes;

namespace GUI_first_iteration
{
    /// <summary>
    /// Interaction logic for EditUserWindow.xaml
    /// </summary>
    public partial class EditUserWindow : Window
    {
        // -----------------------------------
        // DATA MEMBERS ----------------------
        // -----------------------------------

        private MainMenuWindow mainMenuWin;
        private IClientCmd clientCom;
        private UserClass loggedInUser;
        private bool ClosedInCode;

        // -----------------------------------
        // CONSTRUCTOR - EditUserWindow ------
        // -----------------------------------

        /// <summary>
        /// Constructor for EditUserWindow. Referencer til instanserne af de pågældende parametre gemmes som private datamembers. 
        /// Derudover oprettes lægges brugerinformationer om den indloggede bruger ind i de respektive TextBoxes.
        /// </summary>
        /// <param name="mWin">Reference til instansen af MainMenuWindow, der EditUserWindow oprettes fra</param>
        /// <param name="ccom">Reference til instansen af klassen ClienCmd, der står for kommunikation til serveren</param>
        /// <param name="user">Reference til instansen af klassen UserClass, der repræsenterer den indloggede bruger</param>
        public EditUserWindow(MainMenuWindow mWin, IClientCmd ccom, UserClass user)
        {
            // Set private data members
            mainMenuWin = mWin;
            clientCom = ccom;
            loggedInUser = user;
            ClosedInCode = false;


            InitializeComponent();

            // Fill textbox with information about current logged in user
            TbxName.Text = loggedInUser.FirstName;
            TbxSurname.Text = loggedInUser.LastName;
            TbxEmail.Text = loggedInUser.Email;
            TbxPhone.Text = loggedInUser.PhoneNumber;

            // Center window at startup
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        // -----------------------------------
        // BUTTON - Save user ----------------
        // -----------------------------------

        /// <summary>
        /// Funktion der kaldes ved tryk på knap for at gemme brugeroplysninger. Funktionalitet for at gemme ændringer er ikke implementeret.
        /// Denne funktion lukker EditUserWindow og viser MainMenuWindow.
        /// </summary>
        /// <param name="sender">Indeholder information om hvor funktionen kaldes fra.</param>
        /// <param name="e">Indeholder information om eventet der sætter i gang funktionen.</param>
        private void btnSaveUser_Click(object sender, RoutedEventArgs e)
        {
            ClosedInCode = true;
            this.Close();
           mainMenuWin.Show();
        }

        // -----------------------------------
        // BUTTON - Back ---------------------
        // -----------------------------------

        /// <summary>
        /// Funktion der kaldes ved tryk på knap for at gå tilbage til hovedmenuen. Denne funktion lukker EditUserWindow og viser MainMenuWindow.
        /// </summary>
        /// <param name="sender">Indeholder information om hvor funktionen kaldes fra.</param>
        /// <param name="e">Indeholder information om eventet der sætter i gang funktionen.</param>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            // Indicate that the window is closed in code
            ClosedInCode = true;
            this.Close();

            mainMenuWin.Show();
        }

        // -----------------------------------
        // METHOD - Window closing -----------
        // -----------------------------------

        /// <summary>
        /// Funktion der kaldes når vinduet lukkes manuelt. Denne funktion afslutter applikationen. 
        /// </summary>
        /// <param name="sender">Indeholder information om hvor funktionen kaldes fra.</param>
        /// <param name="e">Indeholder information om eventet der sætter i gang funktionen.</param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!ClosedInCode)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
