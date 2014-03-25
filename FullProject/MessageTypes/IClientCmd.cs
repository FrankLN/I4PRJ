using MessageTypes.Messages;
using MessageTypes.ReplyMessages;

namespace ConsoleApplication1
{
    public interface IClientCmd
    {
        //Action metode for all reply messages

        void LoginVerification(ILoginReplyReplyMsg msg);
        void ActivationVerification(IActivationCodeRequestReplyMsg msg);
        void CreateJobVerification(ICreateJobReplyMsg msg);
        void CreateUserVerification(ICreateUserReplyMsg msg);
        void DownloadCommencing(IDownloadJobReplyMsg msg);
        void LoadMaterials(IGetMaterialsReplyMsg msg);
        void LoadJobList(IRequestJobsReplyMsg msg);

        void SendToServer(IMessage msg);
    }
}