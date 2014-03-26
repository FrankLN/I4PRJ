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
using MessageTypes.ReplyMessages;

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
        private UserClass loggedInUser;
        private List<MaterialClass> materialList;  
        private CreateJobMsg createJobObj;
        private JobClass jobObj;


        private bool ClosedInCode;

        // -----------------------------------
        // CONSTRUCTOR - NewJobWindow --------
        // -----------------------------------

        public NewJobWindow(MainMenuWindow mWin, IClientCmd ccom, UserClass user)
        {
            mainMenuWin = mWin;
            clientCom = ccom;
            loggedInUser = user;
            ClosedInCode = false;

            createJobObj = new CreateJobMsg();
            jobObj = new JobClass();



            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            var clientCmd = new ClientCmd();
            clientCmd = (ClientCmd) clientCom;
            clientCmd.onMaterialsMsgReceived += new ClientCmd.LoadMaterialsDelegate(loadMaterialsEvent);
            clientCom.SendToServer(new GetMaterialsMsg());
        }

        // -----------------------------------------------------------
        // Event - Load Materials to the window ----------------------
        // -----------------------------------------------------------

        private void loadMaterialsEvent(IGetMaterialsReplyMsg msg)
        {
            materialList = msg.Materials;
            // The loaded materials should be shown!!!!!!!
   
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

            var clientCmd = new ClientCmd();
            clientCmd = (ClientCmd)clientCom;
            clientCmd.onCreateJobMsgReceived += new ClientCmd.CreateJobDelegate(createJobEvent);
            
            clientCom.SendToServer(createJobObj);
            //clientCmd.sendFileToServer();

        }
        // -----------------------------------------------------------
        // Event - confirm that the server have created the job ------
        // -----------------------------------------------------------
        public void createJobEvent(ICreateJobReplyMsg msg)
        {
            if (msg.Created)
            {
                MessageBox.Show("Your job has been created!");
            }
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
            //createJobObj. = cbxMaterial.SelectionBoxItem.ToString();
        }

        private void cbxHolSol_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //createJobObj.Jo = cbxHolSol.SelectionBoxItem.ToString();
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

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new Microsoft.Win32.OpenFileDialog() { Filter = "STL Files (*.stl)|*.stl|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif" };

            var result = ofd.ShowDialog();

            if (result == false)
            {
                return;
            }

            tbxFilePath.Text = ofd.FileName;
        }
    }
}
