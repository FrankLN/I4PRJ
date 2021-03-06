﻿using System;
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

        private MainMenuWindow mainMenuWin;
        private IClientCom clientCom;
        private CreateUserCom createUserObj;

        private bool ClosedInCode;

        // -----------------------------------
        // CONSTRUCTOR - CreateUserWindow ----
        // -----------------------------------

        public CreateUserWindow(MainMenuWindow mWin, IClientCom ccom)
        {
            mainMenuWin = mWin;
            clientCom = ccom;
            ClosedInCode = false;

            createUserObj = new CreateUserCom();
            InitializeComponent();

            // Center window at startup
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        // -----------------------------------
        // BUTTON - Create user --------------
        // -----------------------------------

        private void btnCreateUser_Click(object sender, RoutedEventArgs e)
        {
            createUserObj.Print();

            ActivateUserWindow activateUserWin = new ActivateUserWindow(this);
            activateUserWin.Show();
        }

        // -----------------------------------
        // BUTTON - Back ---------------------
        // -----------------------------------

        private void btnBack_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mainWin = new MainWindow(mainMenuWin, clientCom);
            mainWin.Show();

            // Indicate that the window is closed in code
            ClosedInCode = true;
            this.Close();
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
            createUserObj.Password = TbxPassword.Password;
        }

        private void TbxPasswordRepeat_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            createUserObj.PasswordRepeat = TbxPasswordRepeat.Password;
        }

        // -----------------------------------
        // METHOD - Window closing -----------
        // -----------------------------------

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Application should only shut down if window was closed manually (alt f4  or  x-button)
            if (!ClosedInCode)
            {
                Application.Current.Shutdown();
            }
            
        }

    }
}
