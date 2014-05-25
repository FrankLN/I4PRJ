using System.Runtime.Serialization;
using MessageTypes;
using MessageTypes.Messages;
using MessageTypes.ReplyMessages;

namespace ClientApplication
{
    /// <summary>
    /// <c>ClientCmd</c> is the class that caretakes all the communication between the GUI and the <c>Client</c>.
    /// </summary>
    public class ClientCmd : IClientCmd
    {

        /// <summary>
        /// <c>LogiDelegate</c> is used with the <c>onLogiMsgReceived</c> event.
        /// </summary>
        /// <param name="msg">The <c>ILoginReplyMsg</c> that should passed to the GUI</param>
        public delegate void LogiDelegate(ILoginReplyMsg msg);

        /// <summary>
        /// <c>ActivationDelegate</c> is used with the <c>onActivationReceived</c> event.
        /// </summary>
        /// <param name="msg">The <c>ActivationDelegate</c> that should passed to the GUI</param>
        public delegate void ActivationDelegate(IActivationCodeRequestReplyMsg msg);

        /// <summary>
        /// <c>CreateJobDelegate</c> is used with the <c>onCreateJobMsgReceived</c> event.
        /// </summary>
        /// <param name="msg">The <c>ICreateJobReplyMsg</c> that should passed to the GUI</param>
        public delegate void CreateJobDelegate(ICreateJobReplyMsg msg);

        /// <summary>
        /// <c>CreateUserDelegate</c> is used with the <c>onCreateUserMsgReceived</c> event.
        /// </summary>
        /// <param name="msg">The <c>ICreateUserReplyMsg</c> that should passed to the GUI</param>
        public delegate void CreateUserDelegate(ICreateUserReplyMsg msg);

        /// <summary>
        /// <c>DownloadDelegate</c> is used with the <c>onDownloadMsgReceived</c> event.
        /// </summary>
        /// <param name="msg">The <c>IDownloadJobReplyMsg</c> that should passed to the GUI</param>
        public delegate void DownloadDelegate(IDownloadJobReplyMsg msg);

        /// <summary>
        /// <c>LoadJobListDelegate</c> is used with the <c>onJobListMsgReceived</c> event.
        /// </summary>
        /// <param name="msg">The <c>IRequestJobsReplyMsg</c> that should passed to the GUI</param>
        public delegate void LoadJobListDelegate(IRequestJobsReplyMsg msg);

        /// <summary>
        /// <c>LoadMaterialsDelegate</c> is used with the <c>onMaterialsMsgReceived</c> event.
        /// </summary>
        /// <param name="msg">The <c>IGetMaterialsReplyMsg</c> that should passed to the GUI</param>
        public delegate void LoadMaterialsDelegate(IGetMaterialsReplyMsg msg);

        /// <summary>
        /// <c>ValidateActivationDelegate</c> is used with the <c>onValidateActivationMsgReceived</c> event.
        /// </summary>
        /// <param name="msg">The <c>IActivationReplyMsg</c> that should passed to the GUI</param>
        public delegate void ValidateActivationDelegate(IActivationReplyMsg msg);
        
        /// <summary>
        /// The <c>event</c> <c>onLogiMsgReceived</c> is triggered when the <c>LoginReplyMsg</c> is received from the <c>Server</c>.
        /// </summary>
        public event LogiDelegate onLogiMsgReceived;

        /// <summary>
        /// The <c>event</c> <c>onActivationReceived</c> is triggered
        /// when the <c>LoginReplyMsg</c> is received from the <c>Server</c>.
        /// </summary>
        public event ActivationDelegate onActivationReceived;

        /// <summary>
        /// The <c>event</c> <c>onCreateJobMsgReceived</c> is triggered
        /// when the <c>CreateJobReplyMsg</c> is received from the <c>Server</c>.
        /// </summary>
        public event CreateJobDelegate onCreateJobMsgReceived;

        /// <summary>
        /// The <c>event</c> <c>onCreateUserMsgReceived</c> is triggered
        /// when the <c>CreateUserReplyMsg</c> is received from the <c>Server</c>.
        /// </summary>
        public event CreateUserDelegate onCreateUserMsgReceived;

        /// <summary>
        /// The <c>event</c> <c>onDownloadMsgReceived</c> is triggered
        /// when the <c>DownloadJobReplyMsg</c> is received from the <c>Server</c>.
        /// </summary>
        public event DownloadDelegate onDownloadMsgReceived;

        /// <summary>
        /// The <c>event</c> <c>onJobListMsgReceived</c> is triggered
        /// when the <c>RequestJobsReplyMsg</c> is received from the <c>Server</c>.
        /// </summary>
        public event LoadJobListDelegate onJobListMsgReceived;

        /// <summary>
        /// The <c>event</c> <c>onMaterialsMsgReceived</c> is triggered
        /// when the <c>GetMaterialsReplyMsg</c> is received from the <c>Server</c>.
        /// </summary>
        public event LoadMaterialsDelegate onMaterialsMsgReceived;

        /// <summary>
        /// The <c>event</c> <c>onValidateActivationMsgReceived</c> is triggered
        /// when the <c>ActivationReplyMsg</c> is received from the <c>Server</c>.
        /// </summary>
        public event ValidateActivationDelegate onValidateActivationMsgReceived;

        private IReplyMessage _replyMessage = null;
        private IClient client = new Client(9000);

        /// <summary>
        /// The <c>FireValidateActivationReply</c> methode triggeres the <c>onValidateActivationMsgReceived</c> event
        /// when the <c>ActivationReplyMsg</c> is received from the <c>Server</c>.
        /// </summary>
        private void FireValidateActivationReply()
        {
            if (onValidateActivationMsgReceived != null)
            {
                onValidateActivationMsgReceived((IActivationReplyMsg) _replyMessage);
            }
        }

        /// <summary>
        /// The <c>FireLogiReply</c> methode triggeres the <c>onValidateActivationMsgReceived</c> event
        /// when the <c>LoginReplyMsg</c> is received from the <c>Server</c>.
        /// </summary>
        private void FireLogiReply()
        {
            if (onLogiMsgReceived != null)
            {
                onLogiMsgReceived((ILoginReplyMsg) _replyMessage);
            }
        }

        /// <summary>
        /// The <c>FireActivationReply</c> methode triggeres the <c>onValidateActivationMsgReceived</c> event
        /// when the <c>ActivationReplyMsg</c> is received from the <c>Server</c>
        /// and passes on the message with the event 
        /// </summary>
        private void FireActivationReply()
        {
            if (onActivationReceived != null)
            {
                onActivationReceived((IActivationCodeRequestReplyMsg) _replyMessage);
            }
        }

        /// <summary>
        /// The <c>FireCreateJobReply</c> methode triggeres the <c>onCreateJobMsgReceived</c> event
        /// when the <c>CreateJobReplyMsg</c> is received from the <c>Server</c>
        /// and passes on the message with the event 
        /// </summary>
        private void FireCreateJobReply()
        {
            if (onCreateJobMsgReceived != null)
            {
                onCreateJobMsgReceived((ICreateJobReplyMsg) _replyMessage);
            }
        }

        /// <summary>
        /// The <c>FireCreateUserReply</c> methode triggeres the <c>onCreateUserMsgReceived</c> event
        /// when the <c>CreateUserReplyMsg</c> is received from the <c>Server</c>
        /// and passes on the message with the event 
        /// </summary>
        private void FireCreateUserReply()
        {
            if (onCreateUserMsgReceived != null)
            {
                onCreateUserMsgReceived((ICreateUserReplyMsg) _replyMessage);
            }
        }

        /// <summary>
        /// The <c>FireDownloadReply</c> methode triggeres the <c>onDownloadMsgReceived</c> event
        /// when the <c>DownloadJobReplyMsg</c> is received from the <c>Server</c>
        /// and passes on the message with the event 
        /// </summary>
        private void FireDownloadReply()
        {
            if (onDownloadMsgReceived != null)
            {
                onDownloadMsgReceived((IDownloadJobReplyMsg) _replyMessage);
            }
        }

        /// <summary>
        /// The <c>FireJobListReply</c> methode triggeres the <c>onJobListMsgReceived</c> event
        /// when the <c>RequestJobsReplyMsg</c> is received from the <c>Server</c>
        /// and passes on the message with the event 
        /// </summary>
        private void FireJobListReply()
        {
            if (onJobListMsgReceived != null)
            {
                onJobListMsgReceived((IRequestJobsReplyMsg) _replyMessage);
            }
        }

        /// <summary>
        /// The <c>FireMaterialsReply</c> methode triggeres the <c>onMaterialsMsgReceived</c> event
        /// when the <c>GetMaterialsReplyMsg</c> is received from the <c>Server</c>
        /// and passes on the message with the event 
        /// </summary>
        private void FireMaterialsReply()
        {
            if (onMaterialsMsgReceived != null)
            {
                onMaterialsMsgReceived((IGetMaterialsReplyMsg) _replyMessage);
            }
        }

        /// <summary>
        /// The <c>LoginVerification</c> methode calls the <c>FireLogiReply</c> methode 
        /// and saves the received <c>ILoginReplyMsg</c>
        /// </summary>
        /// <param name="msg"> Message that should be passed to the GUI</param>
        public void LoginVerification(ILoginReplyMsg msg)
        {
            _replyMessage = (IReplyMessage) msg;
            FireLogiReply();
        }

        /// <summary>
        /// The <c>ActivationVerification</c> methode calls the <c>FireActivationReply</c> methode 
        /// and saves the received <c>IActivationCodeRequestReplyMsg</c>
        /// </summary>
        /// <param name="msg"> Message that should be passed to the GUI</param>
        public void ActivationVerification(IActivationCodeRequestReplyMsg msg)
        {
            _replyMessage = (IReplyMessage) msg;
            FireActivationReply();
        }

        /// <summary>
        /// The <c>CreateJobVerification</c> methode calls the <c>FireCreateJobReply</c> methode 
        /// and saves the received <c>ICreateJobReplyMsg</c>
        /// </summary>
        /// <param name="msg"> Message that should be passed to the GUI</param>
        public void CreateJobVerification(ICreateJobReplyMsg msg)
        {
            _replyMessage = (IReplyMessage) msg;
            FireCreateJobReply();
        }

        /// <summary>
        /// The <c>CreateUserVerification</c> methode calls the <c>FireCreateUserReply</c> methode 
        /// and saves the received <c>ICreateUserReplyMsg</c>
        /// </summary>
        /// <param name="msg"> Message that should be passed to the GUI</param>
        public void CreateUserVerification(ICreateUserReplyMsg msg)
        {
            _replyMessage = (IReplyMessage) msg;
            FireCreateUserReply();
        }

        /// <summary>
        /// The <c>DownloadCommencing</c> methode calls the <c>FireDownloadReply</c> methode 
        /// and saves the received <c>IDownloadJobReplyMsg</c>
        /// </summary>
        /// <param name="msg"> Message that should be passed to the GUI</param>
        public void DownloadCommencing(IDownloadJobReplyMsg msg)
        {
            _replyMessage = (IReplyMessage) msg;
            FireDownloadReply();
        }

        /// <summary>
        /// The <c>LoadMaterials</c> methode calls the <c>FireMaterialsReply</c> methode 
        /// and saves the received <c>IGetMaterialsReplyMsg</c>
        /// </summary>
        /// <param name="msg"> Message that should be passed to the GUI</param>
        public void LoadMaterials(IGetMaterialsReplyMsg msg)
        {
            _replyMessage = (IReplyMessage) msg;
            FireMaterialsReply();
        }

        /// <summary>
        /// The <c>LoadJobList</c> methode calls the <c>FireJobListReply</c> methode 
        /// and saves the received <c>IRequestJobsReplyMsg</c>
        /// </summary>
        /// <param name="msg"> Message that should be passed to the GUI</param>
        public void LoadJobList(IRequestJobsReplyMsg msg)
        {
            _replyMessage = (IReplyMessage) msg;
            FireJobListReply();
        }

        /// <summary>
        /// The <c>ValidateActivation</c> methode calls the <c>FireValidateActivationReply</c> methode 
        /// and saves the received <c>IActivationReplyMsg</c>
        /// </summary>
        /// <param name="msg"> Message that should be passed to the GUI</param>
        public void ValidateActivation(IActivationReplyMsg msg)
        {
            _replyMessage = (IReplyMessage) msg;
            FireValidateActivationReply();
        }

        /// <summary>
        /// The <c>SendToServer</c> methode sends an <c>IMessage</c> to the <c>Server</c>
        /// </summary>
        /// <param name="msg"> <c>IMessage</c> to be send to the <c>Server</c></param>
        public void SendToServer(IMessage msg)
        {

            client.SendToServer((ISerializable)msg);

            if (msg.GetType() == new CreateJobMsg().GetType())
            {
                client.SendFile(((CreateJobMsg)msg).Job.FileSize, ((CreateJobMsg)msg).Job.File) ;
            }

            client.ReceiveMessage().Run(this);
        }

        /// <summary>
        /// The <c>receiveFromFileServer</c> methode calls the <c>ReceiveFile</c> methode
        /// from the the <c>Client</c>.
        /// </summary> 
        /// <param name="fileSize">The size of the file to be send to the <c>Server</c></param>
        /// <param name="fileName">The name of the file to be send to the <c>Server</c></param>
        public void receiveFromFileServer(long fileSize, string name)
        {
            client.ReceiveFile(fileSize,name);
        }

        /// <summary>
        /// The <c>sendFileToServer</c> methode calls the <c>SendFile</c> methode
        /// from the the <c>Client</c>.
        /// </summary> 
        /// <param name="fileSize">The size of the file to be send to the <c>Server</c></param>
        /// <param name="fileName">The name of the file to be send to the <c>Server</c></param>
        public void sendFileToServer(long fileSize, string path)
        {
            client.SendFile(fileSize,path);
        }
    }
}
