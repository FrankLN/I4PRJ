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
     
        public JobDetailsWindow(IClientCmd ccom, UserClass user, JobClass job)
        {
            clientCom = ccom;
            loggedInUser = user;
            currentJob = job;

            InitializeComponent();

            // Center window at startup
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        // -----------------------------------
        // BUTTON -- Back --------------------
        // -----------------------------------

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnDownloadJob_Click(object sender, RoutedEventArgs e)
        {

            // Instead of putting FakeFileName into the downloadJobObj.FileName,
            // we should retreive a filename from the JobClass object we are viewing details for.
            string FakeFileName = "myfile.txt";

            DownloadJobMsg downloadJobObj = new DownloadJobMsg();
            downloadJobObj.FileName = FakeFileName;
            var clientCmd = new ClientCmd();
            clientCmd.onDownloadMsgReceived += new ClientCmd.DownloadDelegate(downloadEvent);
            clientCmd = (ClientCmd) clientCom;
            clientCom.SendToServer(downloadJobObj);
            
        }

        // -----------------------------------
        // Event -- init receive at client---
        // -----------------------------------

        private void downloadEvent(IDownloadJobReplyMsg msg)
        {
            MessageBox.Show("Download started\n Find the file in your download folder!");
            
            clientCom.receiveFromFileServer(msg.FileSize, currentJob.File);

        }
    }
}
