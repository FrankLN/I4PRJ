using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using GUI_first_iteration.Other_Classes;
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
        
        private List<MaterialClass> materialList;  
        private CreateJobMsg createJobObj;
        private JobClass jobObj;
        public MaterialClass selectedMaterial;
        public Hollow selectedHollow;
        public DateTime selectedDate;
        public string comments;


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
            hollowList.Add(new Hollow(){hollow = 0,name = "Fyldt"});
            hollowList.Add(new Hollow(){hollow = 1,name = "Hul"});
            selectedHollow = new Hollow();
            createJobObj = new CreateJobMsg();
            jobObj = new JobClass();

            DataContext = this;

            var clientCmd = new ClientCmd();
            clientCmd = (ClientCmd)clientCom;
            clientCmd.onMaterialsMsgReceived += new ClientCmd.LoadMaterialsDelegate(loadMaterialsEvent);
            clientCom.SendToServer(new GetMaterialsMsg());

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            
        }

        // -----------------------------------------------------------
        // Event - Load Materials to the window ----------------------
        // -----------------------------------------------------------

        private void loadMaterialsEvent(IGetMaterialsReplyMsg msg)
        {
            materialList = msg.Materials;
            MessageBox.Show("material");
            // The loaded materials should be shown!!!!!!!

        }

        // -----------------------------------
        // BUTTON - Create job ---------------
        // -----------------------------------

        private void btnCreateJob_Click(object sender, RoutedEventArgs e)
        {

            jobObj.Material = selectedMaterial;
            jobObj.Deadline = dpDate.SelectedDate.ToString();
            jobObj.Hollow = selectedHollow.hollow;
            jobObj.Deadline = selectedDate.ToString(); // Maybe not correct
            jobObj.Comment = comments;
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

        private void NewJobWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (cbxHolSol != null) cbxHolSol.ItemsSource = hollowList;
            if (cbxMaterial != null) cbxMaterial.ItemsSource = materialsObservableCollection;


        }
    }
}
