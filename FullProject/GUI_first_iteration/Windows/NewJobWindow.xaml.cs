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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using ClientApplication;
using ConsoleApplication1;
using DatabaseInterface;
using MessageTypes.Messages;

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
        private IClientCmd clientCom;
        private ILoggedInUser loggedInUser;

        private CreateJobMsg createJobObj;
        private GetMaterialsMsg getMaterialsObj;
        private JobClass jobObj;


        private bool ClosedInCode;

        // -----------------------------------
        // CONSTRUCTOR - NewJobWindow --------
        // -----------------------------------

        public NewJobWindow(MainMenuWindow mWin, IClientCmd ccom, ILoggedInUser user)
        {
            mainMenuWin = mWin;
            clientCom = ccom;
            loggedInUser = user;
            ClosedInCode = false;

            createJobObj = new CreateJobMsg();
            jobObj = new JobClass();



            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            getMaterialsObj = new GetMaterialsMsg();
            clientCom.SendToServer(getMaterialsObj);
        }

        // -----------------------------------
        // BUTTON - Create job ---------------
        // -----------------------------------

        private void btnCreateJob_Click(object sender, RoutedEventArgs e)
        {
            MaterialClass FakeMaterialObj = new MaterialClass();
            FakeMaterialObj.MaterialType = "Material 1";
            FakeMaterialObj.MaterialId = 0;

            jobObj.Material = FakeMaterialObj;
            jobObj.Deadline = dpDate.SelectedDate.ToString();


            createJobObj.Job = jobObj;

            clientCom.SendToServer(createJobObj);


        }

        // -----------------------------------
        // BUTTON - Back to main menu --------
        // -----------------------------------

        private void btnBackToMain_Click(object sender, RoutedEventArgs e)
        {
            // Indicate that the window is closed in code
            ClosedInCode = true;
            this.Close();

            mainMenuWin.Show();
        }

        private void cbxMaterial_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //createJobObj.Job.Material = cbxMaterial.SelectionBoxItem.ToString();
        }

        private void cbxHolSol_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //createJobObj.HolSol = cbxHolSol.SelectionBoxItem.ToString();
        }

        private void dpDate_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //createJobObj.Date = dpDate.SelectedDate.ToString();
        }

        private void tbxComments_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //createJobObj.Comments = tbxComments.Text;
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
