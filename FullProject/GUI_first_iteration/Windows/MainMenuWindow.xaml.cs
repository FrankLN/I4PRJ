using System.Windows;
using ClientApplication;
using DatabaseInterface;
using MessageTypes;

namespace GUI_first_iteration
{
    /// <summary>
    /// Interaction logic for MainMenuWindow.xaml
    /// </summary>
    public partial class MainMenuWindow : Window
    {
        // -----------------------------------
        // DATA MEMBERS ----------------------
        // -----------------------------------

        private IClientCmd clientCom;
        public UserClass loggedInUser { get; set; }

        // -----------------------------------
        // CONSTRUCTOR - MainMenuWindow ------
        // -----------------------------------

        /// <summary>
        /// Constructor for MainMenuWindow. Dette er det første vindue der oprettes når applikatinen starter.
        /// Der oprettes en instans af klassen ClientCmd, der er interfacet for kommunikation med server. Denne instans gemmes som en private
        /// data member. Derudover skjules MainMenuWindow, MainWindow (login) oprettes og vises.
        /// </summary>
        public MainMenuWindow()
        {
            clientCom = new ClientCmd();

            InitializeComponent();

            // Center window at startup
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            this.Hide();
            MainWindow mainWin = new MainWindow(this, clientCom);
            mainWin.Show();
        }

        // -----------------------------------
        // BUTTON - Log out ------------------
        // -----------------------------------

        /// <summary>
        /// Funktion der kaldes ved tryk på knap for at logge ud. Instansen af klassen UserClass, der repæsenterer den indloggede bruger, nulstilles.
        /// Derudover skjules MainMenuWindow og MainWindow (login) oprettes og vises.
        /// </summary>
        /// <param name="sender">Indeholder information om hvor funktionen kaldes fra.</param>
        /// <param name="e">Indeholder information om eventet der sætter i gang funktionen.</param>
        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            loggedInUser = null;
            this.Hide();
            MainWindow mainWin = new MainWindow(this, clientCom);
            mainWin.Show();
        }

        // -----------------------------------
        // BUTTON - New job ------------------
        // -----------------------------------

        /// <summary>
        /// Funktion der kaldes ved tryk på knap for at oprette en ny job. MainMenuWindow skjules, NewJobWindow oprettes og vises.
        /// </summary>
        /// <param name="sender">Indeholder information om hvor funktionen kaldes fra.</param>
        /// <param name="e">Indeholder information om eventet der sætter i gang funktionen.</param>
        private void btnNewJob_Click(object sender, RoutedEventArgs e)
        {
            NewJobWindow newJobWin = new NewJobWindow(this, clientCom, loggedInUser);
            newJobWin.Show();

            this.Hide();
        }

        // -----------------------------------
        // BUTTON - Job history --------------
        // -----------------------------------

        /// <summary>
        /// Funktion der kaldes ved tryk på knap for at tilgå jobhistorikken. MainMenuWindow skjules, JobHistoryWindow oprettes og vises.
        /// </summary>
        /// <param name="sender">Indeholder information om hvor funktionen kaldes fra.</param>
        /// <param name="e">Indeholder information om eventet der sætter i gang funktionen.</param>
        private void btnJobHistory_Click(object sender, RoutedEventArgs e)
        {
            JobHistoryWindow jobHistoryWin = new JobHistoryWindow(this, clientCom);
            jobHistoryWin.Show();

            this.Hide();
        }

        // -----------------------------------
        // BUTTON - Edit profile --------------
        // -----------------------------------

        /// <summary>
        /// Funktion der kaldes ved tryk på knap for at oprette redigere brugerprofilen. MainMenuWindow skjules, EditUserWindow oprettes og vises.
        /// </summary>
        /// <param name="sender">Indeholder information om hvor funktionen kaldes fra.</param>
        /// <param name="e">Indeholder information om eventet der sætter i gang funktionen.</param>
        private void btnEditProfile_Click(object sender, RoutedEventArgs e)
        {
            EditUserWindow newJobWin = new EditUserWindow(this, loggedInUser);
            newJobWin.Show();

            this.Hide();
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
            Application.Current.Shutdown();
        }
    }
}
