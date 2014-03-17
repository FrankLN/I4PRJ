using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageTypes.Messages;
using MessageTypes.ReplyMessages;

namespace MessageTypes
{
    interface IClientApp
    {
        void VerifyLogin(ILoginReplyReplyMsg loginMsg);
        void CreateUser(ICreateUserReplyMsg createUserMsg);
        void CreateJob(ICreateJobReplyMsg createJobMsg);
        void RequestJobs(IRequestJobsReplyMsg requestJobsMsg);
        void DownloadJob(IDownloadJobReplyMsg downloadJobMsg);
        void GetMaterials(IGetMaterialsReplyMsg getMaterialsMsg);
        void ActivationCodeRequest(IActivationCodeRequestReplyMsg activationCodeRequestMsg);
    }
}
