using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientApplication;
using MessageTypes.ReplyMessages;

namespace ConsoleApplication1
{
    public class ClientCmd : IClientCmd
    {
        public delegate void mydelegate(IReplyMessage msg);
        public delegate void yourdelegate(IActivationCodeRequestReplyMsg msg);
        public event mydelegate onReplyMsgReceived;
        private IReplyMessage _replyMessage = null;

     
        private void FireLogiReply()
        {
            if (onReplyMsgReceived != null)
            {
                onReplyMsgReceived(_replyMessage);
            }
        }

        public void LoginVerification(ILoginReplyReplyMsg msg)
        {
            _replyMessage = (IReplyMessage)msg;
            FireLogiReply();            
        }

        public void ActivationVerification(IActivationCodeRequestReplyMsg msg)
        { 
            _replyMessage = (IReplyMessage)msg;
            FireLogiReply();
        }   

        public void CreateJobVerification(ICreateJobReplyMsg msg)
        {
            _replyMessage = (IReplyMessage)msg;
            FireLogiReply();
        }

        public void CreateUserVerification(ICreateUserReplyMsg msg)
        {
            _replyMessage = (IReplyMessage)msg;
            FireLogiReply();
        }

        public void DownloadCommencing(IDownloadJobReplyMsg msg)
        {
            _replyMessage = (IReplyMessage)msg;
            FireLogiReply();
        }

        public void LoadMaterials(IGetMaterialsReplyMsg msg)
        {
            _replyMessage = (IReplyMessage)msg;
            FireLogiReply();
        }

        public void LoadJobList(IRequestJobsReplyMsg msg)
        {
            _replyMessage = (IReplyMessage)msg;
            FireLogiReply();
        }

        public void ClientCmdRun(IClient client)
        {
            client.ReceiveMessage().Run(this);

        }
    }

}
