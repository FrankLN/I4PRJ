using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConsoleApplication1;
using MessageTypes.Messages;
using MessageTypes.ReplyMessages;
using Server;

namespace ClientApplication.Test
{
    public class FakeClient : IClient, IServerApp
    {

        public IReplyMessage Reply { get; set; }
        private ClientCmd clientCmd;

        public FakeClient(int port)
        {
            MessageBox.Show("FakeClient: SendToServer called with port: " + port.ToString());
            clientCmd = new ClientCmd();
        }
        public void SendToServer(ISerializable message)
        {
            IMessage tmpMsg = (IMessage) message;
            //clientCmd.ClientCmdRun(this);
            //tmpMsg.Run(this);
        }

        public IReplyMessage ReceiveMessage()
        {
            //return Reply;

            throw new NotImplementedException();
        }

        public void ReceiveFile(long fileSize, string fileName)
        {
            throw new NotImplementedException();
        }

        public void SendFile(long fileSize, string path)
        {
            MessageBox.Show("FakeClient: SendFile called");
        }

        public void VerifyLogin(ILoginMsg loginMsg)
        {
            MessageBox.Show("FakeClient: SendToServer called with parameters: " + loginMsg.Email + " " + loginMsg.Password);
        }

        public void CreateUser(ICreateUserMsg createUserMsg)
        {
            MessageBox.Show("FakeClient: SendToServer called with parameters: " + createUserMsg.User.FirstName + " " +createUserMsg.User.Email + " " + createUserMsg.User.AdminRights + " etc...");
        }

        public void CreateJob(ICreateJobMsg createJobMsg)
        {
            MessageBox.Show("FakeClient: SendToServer called with parameters: " + createJobMsg.Job.Deadline + " " +
                            createJobMsg.Job.Material.MaterialType);
        }

        public void RequestJobs(IRequestJobsMsg requestJobsMsg)
        {
            MessageBox.Show("FakeClient: SendToServer called with parameters: " + requestJobsMsg.ToString());
        }

        public void DownloadJob(IDownloadJobMsg downloadJobMsg)
        {
            MessageBox.Show("FakeClient: SendToServer called with parameters: " + downloadJobMsg.FileName);
        }

        public void GetMaterials(IGetMaterialsMsg getMaterialsMsg)
        {
            MessageBox.Show("FakeClient: SendToServer called with parameters: " + getMaterialsMsg.ToString());
        }

        public void ActivationCodeRequest(IActivationCodeRequestMsg activationCodeRequestMsg)
        {
            throw new NotImplementedException();
        }
    }
}
