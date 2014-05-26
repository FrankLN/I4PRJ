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
using ClientApplication;
using ClientApplication;
using DatabaseInterface;
using MessageTypes;
using MessageTypes.Messages;
using MessageTypes.ReplyMessages;

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
        private IClientCmd clientCom;
        private RequestJobsMsg requestJobsObj;
        private UserClass loggedInUser;
        private bool ClosedInCode;
        private List<JobClass> allJobs;

        public JobClass selectedJob { get; set; }

        // -----------------------------------
        // CONSTRUCTOR - JobHistoryWindow ----
        // -----------------------------------

        /// <summary>
        /// Constructor for JobHistoryWindow. Referencer til instanserne af de pågældende parametre gemmes som private datamembers. 
        /// Datacontext sættes til den job der skal ses detaljer for.
        /// </summary>
        /// <param name="mWin">Reference til instansen af MainMenuWindow, der JobHistoryWindow oprettes fra</param>
        /// <param name="ccom">Reference til instansen af klassen ClienCmd, der står for kommunikation til serveren</param>
        /// <param name="user">Reference til instansen af klassen UserClass, der repræsenterer den indloggede bruger</param>
        public JobHistoryWindow(MainMenuWindow mWin, IClientCmd ccom, UserClass user)
        {
            mainMenuWin = mWin;
            clientCom = ccom;
            loggedInUser = user;
            ClosedInCode = false;

            InitializeComponent();
            
            ((ClientCmd)clientCom).onJobListMsgReceived += new ClientCmd.LoadJobListDelegate(LoadJobsEvent);
            requestJobsObj = new RequestJobsMsg();
            clientCom.SendToServer(requestJobsObj);

            this.DataContext = this;

            // Center window at startup
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        // -----------------------------------------------
        // Event - Receives the joblist after requset ----
        // -----------------------------------------------

        /// <summary>
        /// Event der kaldes når serveren svarer på GUIs request om at load jobs. 
        /// Jobs der modtages fra server gemmes som private datamembers, organiseret i en List.
        /// </summary>
        /// <param name="msg">Besked modtaget fra server</param>
        private void LoadJobsEvent(IRequestJobsReplyMsg msg)
        {
            allJobs = msg.JobList;
        }

        // -----------------------------------
        // BUTTON - Back ---------------------
        // -----------------------------------

        /// <summary>
        /// Funktion der kaldes ved tryk på knap for at gå tilbage til hovedmenu. Her lukkes JobHistoryWindow og MainMenuWindow vises
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ((ClientCmd)clientCom).onJobListMsgReceived -= new ClientCmd.LoadJobListDelegate(LoadJobsEvent);

            // Indicate that the window is closed in code
            ClosedInCode = true;
            this.Close();

            mainMenuWin.Show();
        }

        // -----------------------------------
        // BUTTON - Details ------------------
        // -----------------------------------

        /// <summary>
        /// Funktion der kaldes ved tryk på knap for at tilgå detaljer for en bestemt jobu. Funktionen opretter og viser JobDetailsWindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            JobDetailsWindow jobDetailsWin = new JobDetailsWindow(clientCom, loggedInUser, selectedJob);
            jobDetailsWin.Show();
        }


        // -----------------------------------
        // METHOD - Window closing -----------
        // -----------------------------------

        /// <summary>
        /// Funktion der kaldes når vinduet lukkes manuelt. Denne funktion afslutter applikationen. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!ClosedInCode)
            {
                ((ClientCmd)clientCom).onJobListMsgReceived -= new ClientCmd.LoadJobListDelegate(LoadJobsEvent);
                Application.Current.Shutdown();
            }
        }

        // -----------------------------------
        // Methods for databinding to listboxes 
        // -----------------------------------

        /// <summary>
        /// Denne propertys get-metode tilgår den lokale List over alle jobs og sorterer ud de jobs der har status planlagt.
        /// </summary>
        public List<JobClass> ItemsPlanned
        {
            get
            {
                List<JobClass> JobsPlanned = new List<JobClass>();

                foreach (JobClass job in allJobs)
                {
                    if (job.Status == 0)
                    {
                        JobsPlanned.Add(job);
                    }
                }
                return JobsPlanned;
            }
        }

        /// <summary>
        /// Denne propertys get-metode tilgår den lokale List over alle jobs og sorterer ud de jobs der har status igangsat.
        /// </summary>
        public List<JobClass> ItemsInProgress
        {
            get
            {
                List<JobClass> JobsInProgress = new List<JobClass>();

                foreach (JobClass job in allJobs)
                {
                    if (job.Status == 1)
                    {
                        JobsInProgress.Add(job);
                    }
                }
                return JobsInProgress;
            }
        }

        /// <summary>
        /// Denne propertys get-metode tilgår den lokale List over alle jobs og sorterer ud de jobs der har status færdig.
        /// </summary>
        public List<JobClass> ItemsDone
        {
            get
            {
                List<JobClass> JobsDone = new List<JobClass>();

                foreach (JobClass job in allJobs)
                {
                    if (job.Status == 2)
                    {
                        JobsDone.Add(job);
                    }
                }
                return JobsDone;
            }
        }

    }
}
