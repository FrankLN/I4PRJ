using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MessageTypes;
using MessageTypes.Messages;
using MessageTypes.ReplyMessages;

namespace ClientApplication.Test
{
    public class FakeClient : IClient, IServerApp
    {
        public FakeClient(int port)
        {
            MessageBox.Show("FakeClient: SendToServer called with port: " + port.ToString());
        }
        public void SendToServer(ISerializable message)
        {
            IMessage tmpMsg = (IMessage) message;
            //tmpMsg.Run(this);
        }

        public IReplyMessage ReceiveMessage()
        {
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
            MessageBox.Show("FakeClient: SendToServer called with parameters: " + downloadJobMsg.Job.File);
        }

        public void GetMaterials(IGetMaterialsMsg getMaterialsMsg)
        {
            MessageBox.Show("FakeClient: SendToServer called with parameters: " + getMaterialsMsg.ToString());
        }

        public void ActivationCodeRequest(IActivationCodeRequestMsg activationCodeRequestMsg)
        {
            throw new NotImplementedException();
        }

        public void ActivateUser(IActivationMsg activationMsg)
        {
            throw new NotImplementedException();
        }

        public void VerifyLogin(ILoginMsg loginMsg, IServer server)
        {
            throw new NotImplementedException();
        }

        public void CreateUser(ICreateUserMsg createUserMsg, IServer server)
        {
            throw new NotImplementedException();
        }

        public void CreateJob(ICreateJobMsg createJobMsg, IServer server)
        {
            throw new NotImplementedException();
        }

        public void RequestJobs(IRequestJobsMsg requestJobsMsg, IServer server)
        {
            throw new NotImplementedException();
        }

        public void DownloadJob(IDownloadJobMsg downloadJobMsg, IServer server)
        {
            throw new NotImplementedException();
        }

        public void GetMaterials(IGetMaterialsMsg getMaterialsMsg, IServer server)
        {
            throw new NotImplementedException();
        }

        public void ActivationCodeRequest(IActivationCodeRequestMsg activationCodeRequestMsg, IServer server)
        {
            throw new NotImplementedException();
        }

        public void ActivateUser(IActivationMsg activationMsg, IServer server)
        {
            throw new NotImplementedException();
        }
    }
}
