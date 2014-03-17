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
    /// Interaction logic for MainMenuWindow.xaml
    /// </summary>
    public partial class MainMenuWindow : Window
    {
        // -----------------------------------
        // DATA MEMBERS ----------------------
        // -----------------------------------

        private MainWindow main_parent;
        private IClientCom clientCom;
        private ILoggedInUser loggedInUser;

        // -----------------------------------
        // CONSTRUCTOR -----------------------
        // -----------------------------------

        public MainMenuWindow(MainWindow parent, IClientCom ccom)
        {
            main_parent = parent;
            clientCom = ccom;

            loggedInUser = new LoggedInUser();
            loggedInUser.FirstName = "Jonas";
            loggedInUser.Surname = "Ulleberg";
            loggedInUser.Email = "test@iha.dk";
            loggedInUser.Phone = "60535052";

            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        // -----------------------------------
        // BUTTON - Log out ------------------
        // -----------------------------------

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            main_parent.Show();
            this.Hide();
            
        }

        // -----------------------------------
        // BUTTON - New job ------------------
        // -----------------------------------

        private void btnNewJob_Click(object sender, RoutedEventArgs e)
        {
            NewJobWindow newJobWin = new NewJobWindow(this, clientCom);
            newJobWin.Show();

            this.Hide();

        }

        // -----------------------------------
        // BUTTON - Job history --------------
        // -----------------------------------

        private void btnJobHistory_Click(object sender, RoutedEventArgs e)
        {
            JobHistoryWindow jobHistoryWin = new JobHistoryWindow(this, clientCom, loggedInUser);
            jobHistoryWin.Show();

            this.Hide();
        }

        private void btnEditProfile_Click(object sender, RoutedEventArgs e)
        {
            EditUserWindow newJobWin = new EditUserWindow(this, clientCom, loggedInUser);
            newJobWin.Show();

            this.Hide();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
