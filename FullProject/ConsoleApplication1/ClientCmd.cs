using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ClientApplication;
using MessageTypes.Messages;
using MessageTypes.ReplyMessages;

namespace ConsoleApplication1
{
    public class ClientCmd : IClientCmd
    {
        public delegate void LogiDelegate(ILoginReplyReplyMsg msg);

        public delegate void ActivationDelegate(IActivationCodeRequestReplyMsg msg);

        public delegate void CreateJobDelegate(ICreateJobReplyMsg msg);

        public delegate void CreateUserDelegate(ICreateUserReplyMsg msg);

        public delegate void DownloadDelegate(IDownloadJobReplyMsg msg);

        public delegate void LoadJobListDelegate(IRequestJobsReplyMsg msg);

        public delegate void LoadMaterialsDelegate(IGetMaterialsReplyMsg msg);

        public event LogiDelegate onLogiMsgReceived;
        public event ActivationDelegate onActivationReceived;
        public event CreateJobDelegate onCreateJobMsgReceived;
        public event CreateUserDelegate onCreateUserMsgReceived;
        public event DownloadDelegate onDownloadMsgReceived;
        public event LoadJobListDelegate onJobListMsgReceived;
        public event LoadMaterialsDelegate onMaterialsMsgReceived;


        private IReplyMessage _replyMessage = null;
        private IClient client = new Client(9000);

        private void FireLogiReply()
        {
            if (onLogiMsgReceived != null)
            {
                onLogiMsgReceived((ILoginReplyReplyMsg) _replyMessage);
            }
        }

        private void FireActivationReply()
        {
            if (onActivationReceived != null)
            {
                onActivationReceived((IActivationCodeRequestReplyMsg) _replyMessage);
            }
        }

        private void FireCreateJobReply()
        {
            if (onCreateJobMsgReceived != null)
            {
                onCreateJobMsgReceived((ICreateJobReplyMsg) _replyMessage);
            }
        }

        private void FireCreateUserReply()
        {
            if (onCreateUserMsgReceived != null)
            {
                onCreateUserMsgReceived((ICreateUserReplyMsg) _replyMessage);
            }
        }

        private void FireDownloadReply()
        {
            if (onDownloadMsgReceived != null)
            {
                onDownloadMsgReceived((IDownloadJobReplyMsg) _replyMessage);
            }
        }

        private void FireJobListReply()
        {
            if (onJobListMsgReceived != null)
            {
                onJobListMsgReceived((IRequestJobsReplyMsg) _replyMessage);
            }
        }

        private void FireMaterialsReply()
        {
            if (onMaterialsMsgReceived != null)
            {
                onMaterialsMsgReceived((IGetMaterialsReplyMsg) _replyMessage);
            }
        }

        public void LoginVerification(ILoginReplyReplyMsg msg)
        {
            _replyMessage = (IReplyMessage) msg;
            FireLogiReply();
        }

        public void ActivationVerification(IActivationCodeRequestReplyMsg msg)
        {
            _replyMessage = (IReplyMessage) msg;
            FireActivationReply();
        }

        public void CreateJobVerification(ICreateJobReplyMsg msg)
        {
            _replyMessage = (IReplyMessage) msg;
            FireCreateJobReply();
        }

        public void CreateUserVerification(ICreateUserReplyMsg msg)
        {
            _replyMessage = (IReplyMessage) msg;
            FireCreateUserReply();
        }

        public void DownloadCommencing(IDownloadJobReplyMsg msg)
        {
            _replyMessage = (IReplyMessage) msg;
            FireDownloadReply();
        }


        public void LoadMaterials(IGetMaterialsReplyMsg msg)
        {
            _replyMessage = (IReplyMessage) msg;
            FireMaterialsReply();
        }

        public void LoadJobList(IRequestJobsReplyMsg msg)
        {
            _replyMessage = (IReplyMessage) msg;
            FireJobListReply();
        }


        public void SendToServer(IMessage msg)
        {
            client.SendToServer((ISerializable) msg);
            client.ReceiveMessage().Run(this);
        }

        public void receiveFromFileServer(long fileSize, string name )
        {
            client.ReceiveFile(fileSize,name);
        }

        public void sendFileToServer(long fileSize, string path)
        {
            client.SendFile(fileSize,path);
        }
    }
}
