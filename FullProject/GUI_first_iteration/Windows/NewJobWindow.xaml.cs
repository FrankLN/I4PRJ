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

        // -----------------------------------------------------------
        // Constructor - New Job Window ------------------------------
        // -----------------------------------------------------------

        /// <summary>
        /// Constructor for NewJobWindow. Referencer til instanserne af de pågældende parametre gemmes som private datamembers. 
        /// Derudover oprettes en instans af klassen CreateJobMsg, samt en instans af JobClass, der skal sendes til server. Datacontexten sættes til at pege på vinduets egen instans.
        /// Der sendes et request til server for at modtage de tilgængelige materialer. Videre lægges de tilgængelige alternaativer for "hollow" ind i ListBox controlleren.
        /// </summary>
        /// <param name="mWin">Reference til instansen af MainMenuWindow, der NewJobWindow oprettes fra</param>
        /// <param name="ccom">Reference til instansen af klassen ClienCmd, der står for kommunikation til serveren</param>
        /// <param name="user">Reference til instansen af klassen UserClass, der repræsenterer den indloggede bruger</param>
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

        /// <summary>
        /// Event der kaldes når serveren svarer på GUIs request om at få tilgængelige materialer. Når materialer modtages, lægges disse ind
        /// i en liste som er gemt i vinduets egen instans
        /// </summary>
        /// <param name="msg">Besked modtaget fra serveren</param>
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

        /// <summary>
        /// Funktion der kaldes ved tryk på knap for at oprette job. De alternativer der er valgt for en job lægges ind i isntansen af klassen JobClass
        /// Derudover lægges denne instansen ind i instansen af klassen CreateJobMsg, før denne så sendes til server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateJob_Click(object sender, RoutedEventArgs e)
        {
            jobObj.Material = selectedMaterial;
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
        }
        
        // -----------------------------------------------------------
        // Event - confirm that the server have created the job ------
        // -----------------------------------------------------------

        /// <summary>
        /// Event der kaldes når serveren svarer på GUIs request om at oprette en job. Der oprettes en MessageBox der informerer om oprettelsen var vellykket eller ikke
        /// i en liste som er gemt i vinduets egen instans
        /// </summary>
        /// <param name="msg">Besked modtaget fra serveren</param>
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

        /// <summary>
        /// Funktion der kaldes ved tryk på knap for at gå tilbage til hovedmenu. Her lukkes vinduet og MainMenuWindow vises
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBackToMain_Click(object sender, RoutedEventArgs e)
        {
            // Indicate that the window is closed in code
            ClosedInCode = true;
            this.Close();

            mainMenuWin.Show();
        }

        // -----------------------------------
        // METHOD - Window closing -----------
        // -----------------------------------

        /// <summary>
        /// Funktion der kaldes når vinduet lukkes. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!ClosedInCode)
            {
                ((ClientCmd)clientCom).onMaterialsMsgReceived -= new ClientCmd.LoadMaterialsDelegate(loadMaterialsEvent);
                ((ClientCmd)clientCom).onCreateJobMsgReceived -= new ClientCmd.CreateJobDelegate(createJobEvent);
                Application.Current.Shutdown();
            }
        }

        /// <summary>
        /// Funktion der kaldes ved tryk på knap for at uploade et job. Her bestemmes hvilken type filer kan uploades og den valgte 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new Microsoft.Win32.OpenFileDialog() { Filter = "STL Files (*.stl)|*.stl|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif" };

            var result = ofd.ShowDialog();

            if (result == false)
            {
                return;
            }

            selectedFile = ofd.FileName;
           // tbxFilePath.Text = selectedFile;

        }

    }
}
