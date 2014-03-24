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
        public delegate void LogiDelegate(ILoginReplyReplyMsg msg);
        public delegate void CreateJobDelegate(ICreateJobReplyMsg msg);
        public delegate void CreateUserDelegate(IActivationCodeRequestReplyMsg msg);
        public delegate void DownloadDelegate(IActivationCodeRequestReplyMsg msg);
        public delegate void LoadJobListDelegate(IActivationCodeRequestReplyMsg msg);
        public delegate void LoadMaterialsDelegate(IActivationCodeRequestReplyMsg msg);

        public event LogiDelegate onLogiMsgReceived;
        public event CreateJobDelegate onCreateJobMsgReceived;
        public event CreateUserDelegate onCreateUserMsgReceived;
        public event DownloadDelegate onDownloadMsgReceived;
        public event LoadJobListDelegate onJobListMsgReceived;
        public event LoadMaterialsDelegate onMaterialsMsgReceived;


        private IReplyMessage _replyMessage = null;


        private void FireLogiReply()
        {
            if (onLogiMsgReceived != null)
            {
                onLogiMsgReceived((ILoginReplyReplyMsg)_replyMessage);
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
