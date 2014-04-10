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
using ConsoleApplication1;
using DatabaseInterface;
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
        private void LoadJobsEvent(IRequestJobsReplyMsg msg)
        {
            allJobs = msg.JobList;
        }

        // -----------------------------------
        // BUTTON - Back ---------------------
        // -----------------------------------

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

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            JobDetailsWindow jobDetailsWin = new JobDetailsWindow(clientCom, loggedInUser, selectedJob);
            jobDetailsWin.Show();
        }


        // -----------------------------------
        // METHOD - Window closing -----------
        // -----------------------------------

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
