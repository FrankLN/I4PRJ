using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageTypes.ReplyMessages;

namespace ConsoleApplication1
{
    public interface IClientCmd
    {
        //MsgHandler + action metode for all reply messages
        void MsgHandler(IReplyMessage replyMsg);
        void LoginVerification();
        void ActivationVerification();
        void CreateJobVerification();
        void CreateUserVerification();
        void DownloadCommencing();
        void LoadMaterials();
        void LoadJobList();
    }

    class ClientCmd
    {

    }
}
