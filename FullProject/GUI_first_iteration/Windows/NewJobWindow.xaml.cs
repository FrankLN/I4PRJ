using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
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
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;
using ClientApplication;
using ClientApplication;
using DatabaseInterface;
using MessageTypes;
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
        private ObservableCollection<MaterialClass> materialsObservableCollection = new ObservableCollection<MaterialClass>();
        private ObservableCollection<Hollow> hollowList = new ObservableCollection<Hollow>();
        private List<MaterialClass> materialList = new List<MaterialClass>();
        private CreateJobMsg createJobObj;
        private JobClass jobObj;
        public MaterialClass selectedMaterial { get; set; }
        public Hollow selectedHollow{ get; set; }
        public DateTime selectedDate { get; set; }
    
        public string selectedComment { get; set; }
        public string selectedFile { get; set; }


        private bool ClosedInCode;

        // -----------------------------------
        // CONSTRUCTOR - NewJobWindow --------
        // -----------------------------------

        public NewJobWindow(MainMenuWindow mWin, IClientCmd ccom, UserClass user)
        {
            InitializeComponent();

            mainMenuWin = mWin;
            clientCom = ccom;
            loggedInUser = user;
            ClosedInCode = false;
            // Material combobox
            selectedMaterial = new MaterialClass();
            // Hollow combobox - add elements to the box
            hollowList.Add(new Hollow(){hollow = 100,name = "Fyldt"});
            hollowList.Add(new Hollow(){hollow = 0,name = "Hul"});
            selectedHollow = new Hollow();
            createJobObj = new CreateJobMsg();
            jobObj = new JobClass();

            DataContext = this;

            ((ClientCmd)clientCom).onMaterialsMsgReceived += new ClientCmd.LoadMaterialsDelegate(loadMaterialsEvent);
            ((ClientCmd)clientCom).onCreateJobMsgReceived += new ClientCmd.CreateJobDelegate(createJobEvent);
            
            clientCom.SendToServer(new GetMaterialsMsg());
           
            if (cbxHolSol != null) cbxHolSol.ItemsSource = hollowList;
            if (cbxMaterial != null) cbxMaterial.ItemsSource = materialsObservableCollection;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            
        }

        // -----------------------------------------------------------
        // Event - Load Materials to the window ----------------------
        // -----------------------------------------------------------

        private void loadMaterialsEvent(IGetMaterialsReplyMsg msg)
        {
            foreach (var e in msg.Materials)
            {
                materialsObservableCollection.Add(e);
            }

        }

        // -----------------------------------
        // BUTTON - Create job ---------------
        // -----------------------------------

        private void btnCreateJob_Click(object sender, RoutedEventArgs e)
        {

            jobObj.Material = selectedMaterial;
            //jobObj.Deadline = dpDate.SelectedDate.ToString();
            jobObj.Hollow = selectedHollow != null ? selectedHollow.hollow : 0;
            jobObj.Deadline = selectedDate.ToShortDateString(); // Maybe not correct
            jobObj.Comment = selectedComment;
            jobObj.File = selectedFile;
            jobObj.Owner = loggedInUser;
            jobObj.FileSize = selectedFile != null ? new FileInfo(selectedFile).Length : 0;
            
            createJobObj.Job = jobObj;

            var clientCmd = new ClientCmd();
            clientCmd = (ClientCmd)clientCom;
            

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
            else
            {
                MessageBox.Show("Something went wrong when trying to create the job. Please try again.");
                
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


        private void cbxHolSol_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //createJobObj.Jo = cbxHolSol.SelectionBoxItem.ToString();
        }

        private void dpDate_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //createJobObj.Date = dpDate.SelectedDate.ToString();
        }

      

        // -----------------------------------
        // METHOD - Window closing -----------
        // -----------------------------------

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!ClosedInCode)
            {
                ((ClientCmd)clientCom).onMaterialsMsgReceived -= new ClientCmd.LoadMaterialsDelegate(loadMaterialsEvent);
                ((ClientCmd)clientCom).onCreateJobMsgReceived -= new ClientCmd.CreateJobDelegate(createJobEvent);
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

            selectedFile = ofd.FileName;
            tbxFilePath.Text = selectedFile;

        }

    }
}
