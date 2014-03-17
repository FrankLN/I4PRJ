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
    /// Interaction logic for CreateUserWindow.xaml
    /// </summary>
    public partial class CreateUserWindow : Window
    {
        // -----------------------------------
        // DATA MEMBERS ----------------------  
        // -----------------------------------

        private MainWindow main_parent;
        private IClientCom clientCom;
        private CreateUserCom createUserObj;

        // -----------------------------------
        // CONSTRUCTOR - CreateUserWindow ----
        // -----------------------------------

        public CreateUserWindow(MainWindow parent, IClientCom ccom)
        {
            main_parent = parent;
            clientCom = ccom;
            createUserObj = new CreateUserCom();
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        // -----------------------------------
        // BUTTON - Create user --------------
        // -----------------------------------

        private void btnCreateUser_Click(object sender, RoutedEventArgs e)
        {
            createUserObj.Print();
        }

        // -----------------------------------
        // BUTTON - Back ---------------------
        // -----------------------------------

        private void btnBack_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();
            main_parent.Show();
        }

        // ----------------------------------------------------------------------------
        // Methods for updating class with content of textbox, upon leaving the textbox
        // ----------------------------------------------------------------------------

        private void TbxName_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            createUserObj.FirstName = TbxName.Text;
        }

        private void TbxSurname_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            createUserObj.Surname = TbxSurname.Text;
        }

        private void TbxEmail_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            createUserObj.Email = TbxEmail.Text;
        }

        private void TbxPhone_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            createUserObj.Phone = TbxPhone.Text;
        }

        private void TbxPassword_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            createUserObj.Password = TbxPassword.Text;
        }

        private void TbxPasswordRepeat_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            createUserObj.PasswordRepeat = TbxPasswordRepeat.Text;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
