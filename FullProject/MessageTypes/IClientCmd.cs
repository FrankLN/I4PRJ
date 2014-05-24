using MessageTypes.Messages;
using MessageTypes.ReplyMessages;

namespace MessageTypes
{
    /// <summary>
    /// Interface for <c>ClientCmd</c>.
    /// </summary>
    public interface IClientCmd
    {
        /// <summary>
        /// Action method <c>LoginVerification</c>.
        /// </summary>
        /// <param name="msg">Message recieved.</param>
        void LoginVerification(ILoginReplyMsg msg);

        /// <summary>
        /// Action method <c>ActivationVerification</c>.
        /// </summary>
        /// <param name="msg">Message recieved.</param>
        void ActivationVerification(IActivationCodeRequestReplyMsg msg);

        /// <summary>
        /// Action method <c>CreateJobVerification</c>.
        /// </summary>
        /// <param name="msg">Message recieved.</param>
        void CreateJobVerification(ICreateJobReplyMsg msg);

        /// <summary>
        /// Action method <c>CreateUserVerification</c>.
        /// </summary>
        /// <param name="msg">Message recieved.</param>
        void CreateUserVerification(ICreateUserReplyMsg msg);

        /// <summary>
        /// Action method <c>DownloadCommencing</c>.
        /// </summary>
        /// <param name="msg">Message recieved.</param>
        void DownloadCommencing(IDownloadJobReplyMsg msg);

        /// <summary>
        /// Action method <c>LoadMaterials</c>.
        /// </summary>
        /// <param name="msg">Message recieved.</param>
        void LoadMaterials(IGetMaterialsReplyMsg msg);

        /// <summary>
        /// Action method <c>LoadJobList</c>.
        /// </summary>
        /// <param name="msg">Message recieved.</param>
        void LoadJobList(IRequestJobsReplyMsg msg);

        /// <summary>
        /// Action method <c>ValidateActivation</c>.
        /// </summary>
        /// <param name="msg">Message recieved.</param>
        void ValidateActivation(IActivationReplyMsg msg);

        /// <summary>
        /// Action method <c>SendToServer</c>.
        /// </summary>
        /// <param name="msg">Message recieved.</param>
        void SendToServer(IMessage msg);

        /// <summary>
        /// Method signature for method <c>recieveFromFileServer</c>.
        /// </summary>
        /// <param name="fileSize">Size of the file.</param>
        /// <param name="name">Name of the file.</param>
        void receiveFromFileServer(long fileSize, string name);

        /// <summary>
        /// Method signature for method <c>SentFileToServer</c>.
        /// </summary>
        /// <param name="fileSize">Size of the file.</param>
        /// <param name="path">Name of the file.</param>
        void sendFileToServer(long fileSize, string path);
    }
}