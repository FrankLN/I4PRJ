using System.Windows;
using ClientApplication;
using DatabaseInterface;
using MessageTypes;
using MessageTypes.Messages;
using MessageTypes.ReplyMessages;

namespace GUI_first_iteration
{
    /// <summary>
    /// Interaction logic for JobDetailsWindow.xaml
    /// </summary>
    public partial class JobDetailsWindow : Window
    {
        // -----------------------------------
        // DATA MEMBERS ----------------------
        // -----------------------------------

        private IClientCmd clientCom;
        private UserClass loggedInUser;
        private JobClass currentJob;

        // -----------------------------------
        // CONSTRUCTOR - JobDetailsWindow ----
        // -----------------------------------

        /// <summary>
        /// Constructor for JobDetailsWindow. Referencer til instanserne af de pågældende parametre gemmes som private datamembers. 
        /// Datacontext sættes til den job der skal ses detaljer for. 
        /// </summary>
        /// <param name="ccom">Reference til instansen af klassen ClienCmd, der står for kommunikation til serveren</param>
        /// <param name="user">Reference til instansen af klassen UserClass, der repræsenterer den indloggede bruger</param>
        /// <param name="job">Reference til instansen af klassen JobClass, der repræsenterer den job der skal ses detaljer for</param>
        public JobDetailsWindow(IClientCmd ccom, UserClass user, JobClass job)
        {
            clientCom = ccom;
            loggedInUser = user;
            currentJob = job;

            InitializeComponent();

            //Databinding
            DataContext = currentJob;
         
            // Center window at startup
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            ((ClientCmd)clientCom).onDownloadMsgReceived += new ClientCmd.DownloadDelegate(downloadEvent);
        }

        // -----------------------------------
        // BUTTON -- Back --------------------
        // -----------------------------------
       
        /// <summary>
        /// Funktion der kaldes ved tryk på knap for at gå tilbage JobHistoryWindow. Denne funktion lukker JobDetailsWindow.
        /// </summary>
        /// <param name="sender">Indeholder information om hvor funktionen kaldes fra.</param>
        /// <param name="e">Indeholder information om eventet der sætter i gang funktionen.</param>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ((ClientCmd)clientCom).onDownloadMsgReceived -= new ClientCmd.DownloadDelegate(downloadEvent);
            this.Close();
        }

        // -----------------------------------
        // BUTTON -- Download Job ------------
        // -----------------------------------

        /// <summary>
        /// Funktion der kaldes ved tryk på knap for at downloade job. Denne funktionen lægger en JobClass instans for den job der skal downloades,
        /// ind i DownloadJobMsg, før denne sendes til server.
        /// </summary>
        /// <param name="sender">Indeholder information om hvor funktionen kaldes fra.</param>
        /// <param name="e">Indeholder information om eventet der sætter i gang funktionen.</param>
        private void btnDownloadJob_Click(object sender, RoutedEventArgs e)
        {

            // Instead of putting FakeFileName into the downloadJobObj.FileName,
            // we should retreive a filename from the JobClass object we are viewing details for.

            DownloadJobMsg downloadJobObj = new DownloadJobMsg();
            
            
            downloadJobObj.Job = currentJob;
            clientCom.SendToServer(downloadJobObj);

        }

        // -----------------------------------
        // Event -- init receive at client---
        // -----------------------------------

        /// <summary>
        /// Event der kaldes når serveren svarer på GUIs request om at downloade en job. 
        /// Bruger får besked om at download er startet. Funktionen receiveFromFileServer() kaldes på instansen av ClientCmd-klassen,
        /// for at sætte i gang download af den aktuelle job.
        /// </summary>
        /// <param name="msg">Besked modtaget fra server</param>
        private void downloadEvent(IDownloadJobReplyMsg msg)
        {
            MessageBox.Show("Download started\n Find the file in your download folder!");
            
            clientCom.receiveFromFileServer(msg.Job.FileSize, currentJob.File);

        }
    }
}
