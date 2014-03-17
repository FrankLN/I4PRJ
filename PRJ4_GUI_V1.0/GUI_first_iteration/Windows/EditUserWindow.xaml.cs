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
    /// Interaction logic for EditUserWindow.xaml
    /// </summary>
    public partial class EditUserWindow : Window
    {
        // -----------------------------------
        // DATA MEMBERS ----------------------
        // -----------------------------------

        private MainMenuWindow main_parent;
        private IClientCom clientCom;
        private ILoggedInUser loggedInUser;
        private EditUserCom editUserObj;

        // -----------------------------------
        // CONSTRUCTOR - EditUserWindow ------
        // -----------------------------------

        public EditUserWindow(MainMenuWindow parent, IClientCom ccom, ILoggedInUser user)
        {
            main_parent = parent;
            clientCom = ccom;
            loggedInUser = user;

            InitializeComponent();

            TbxName.Text = user.FirstName;
            TbxSurname.Text = user.Surname;
            TbxEmail.Text = user.Email;
            TbxPhone.Text = user.Phone;

            editUserObj = new EditUserCom();

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        // -----------------------------------
        // BUTTON - Save user ----------------
        // -----------------------------------

        private void btnSaveUser_Click(object sender, RoutedEventArgs e)
        {
            editUserObj.Print();
        }

        // -----------------------------------
        // BUTTON - Back ---------------------
        // -----------------------------------

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            main_parent.Show();
        }

        // ----------------------------------------------------------------------------
        // Methods for updating class with content of textbox, upon leaving the textbox
        // ----------------------------------------------------------------------------

        private void TbxName_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            editUserObj.FirstName = TbxName.Text;
        }

        private void TbxSurname_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            editUserObj.Surname = TbxSurname.Text;
        }

        private void TbxEmail_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            editUserObj.Email = TbxEmail.Text;
        }

        private void TbxPhone_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            editUserObj.Phone = TbxPhone.Text;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
