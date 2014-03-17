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
    /// Interaction logic for JobHistoryWindow.xaml
    /// </summary>
    public partial class JobHistoryWindow : Window
    {
        // -----------------------------------
        // DATA MEMBERS ----------------------
        // -----------------------------------

        private MainMenuWindow main_parent;
        private IClientCom clientCom;
        private JobHistoryCom jobHistoryObj;
        private ILoggedInUser loggedInUser;

        // -----------------------------------
        // CONSTRUCTOR -----------------------
        // -----------------------------------

        public JobHistoryWindow(MainMenuWindow parent, IClientCom ccom, ILoggedInUser user)
        {
            main_parent = parent;
            clientCom = ccom;
            loggedInUser = user;

            jobHistoryObj = new JobHistoryCom();
            jobHistoryObj.Email = loggedInUser.Email;
            jobHistoryObj.Print();

            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            JobDetailsWindow jobDetailsWin = new JobDetailsWindow(this, clientCom);

            jobDetailsWin.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
