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
    /// Interaction logic for NewJobWindow.xaml
    /// </summary>
    public partial class NewJobWindow : Window
    {
        // -----------------------------------
        // DATA MEMBERS ----------------------
        // -----------------------------------

        private MainMenuWindow mainMenuWin;
        private IClientCom clientCom;
        private ILoggedInUser loggedInUser;
        private NewJobCom newJobObj;
        private bool ClosedInCode;

        // -----------------------------------
        // CONSTRUCTOR -----------------------
        // -----------------------------------

        public NewJobWindow(MainMenuWindow mWin, IClientCom ccom, ILoggedInUser user)
        {
            mainMenuWin = mWin;
            clientCom = ccom;
            loggedInUser = user;
            ClosedInCode = false;

            newJobObj = new NewJobCom();
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        // -----------------------------------
        // BUTTON - Create job ---------------
        // -----------------------------------

        private void btnCreateJob_Click(object sender, RoutedEventArgs e)
        {
            newJobObj.Print();
        }

        private void btnBackToMain_Click(object sender, RoutedEventArgs e)
        {
            // Indicate that the window is closed in code
            ClosedInCode = true;
            this.Close();

            mainMenuWin.Show();
        }

        private void cbxMaterial_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            newJobObj.Material = cbxMaterial.SelectionBoxItem.ToString();
        }

        private void cbxHolSol_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            newJobObj.HolSol = cbxHolSol.SelectionBoxItem.ToString();
        }

        private void dpDate_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            newJobObj.Date = dpDate.SelectedDate.ToString();
        }

        private void tbxComments_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            newJobObj.Comments = tbxComments.Text;
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
