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
    /// Interaction logic for JobHistoryWindow.xaml
    /// </summary>
    public partial class JobHistoryWindow : Window
    {
        // -----------------------------------
        // DATA MEMBERS ----------------------
        // -----------------------------------

        private MainMenuWindow mainMenuWin;
        private IClientCom clientCom;
        private JobHistoryCom jobHistoryObj;
        private ILoggedInUser loggedInUser;
        private bool ClosedInCode;

        // -----------------------------------
        // CONSTRUCTOR -----------------------
        // -----------------------------------

        public JobHistoryWindow(MainMenuWindow mWin, IClientCom ccom, ILoggedInUser user)
        {
            mainMenuWin = mWin;
            clientCom = ccom;
            loggedInUser = user;
            ClosedInCode = false;

            jobHistoryObj = new JobHistoryCom();
            jobHistoryObj.Email = loggedInUser.Email;
            jobHistoryObj.Print();

            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            JobDetailsWindow jobDetailsWin = new JobDetailsWindow(clientCom, loggedInUser);

            jobDetailsWin.Show();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            // Indicate that the window is closed in code
            ClosedInCode = true;
            this.Close();

            mainMenuWin.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!ClosedInCode)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
