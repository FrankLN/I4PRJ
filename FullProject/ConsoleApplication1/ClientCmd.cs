using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageTypes.ReplyMessages;

namespace ConsoleApplication1
{
    public class ClientCmd : IClientCmd
    {
        public void LoginVerification(ILoginReplyReplyMsg msg)
        {
            throw new NotImplementedException();
        }

        public void ActivationVerification(IActivationCodeRequestReplyMsg msg)
        {
            throw new NotImplementedException();
        }

        public void CreateJobVerification(ICreateJobReplyMsg msg)
        {
            throw new NotImplementedException();
        }

        public void CreateUserVerification(ICreateUserReplyMsg msg)
        {
            throw new NotImplementedException();
        }

        public void DownloadCommencing(IDownloadJobReplyMsg msg)
        {
            throw new NotImplementedException();
        }

        public void LoadMaterials(IGetMaterialsReplyMsg msg)
        {
            throw new NotImplementedException();
        }

        public void LoadJobList(IRequestJobsReplyMsg msg)
        {
            throw new NotImplementedException();
        }
    }
}
