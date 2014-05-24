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
using ClientApplication;
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

        private void btnSaveUser_Click(object sender, RoutedEventArgs e)
        {
            ClosedInCode = true;
            this.Close();
           mainMenuWin.Show();
        }

        // -----------------------------------
        // BUTTON - Back ---------------------
        // -----------------------------------

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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!ClosedInCode)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
