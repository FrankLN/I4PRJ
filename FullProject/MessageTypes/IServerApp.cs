using MessageTypes.Messages;

namespace MessageTypes
{
	//Interface which define which methodes we need to use on the server
	public interface IServerApp
	{
        void VerifyLogin(ILoginMsg loginMsg, IServer server);
        void CreateUser(ICreateUserMsg createUserMsg, IServer server);
        void CreateJob(ICreateJobMsg createJobMsg, IServer server);
        void RequestJobs(IRequestJobsMsg requestJobsMsg, IServer server);
        void DownloadJob(IDownloadJobMsg downloadJobMsg, IServer server);
        void GetMaterials(IGetMaterialsMsg getMaterialsMsg, IServer server);
	    void ActivationCodeRequest(IActivationCodeRequestMsg activationCodeRequestMsg, IServer server);
        void ActivateUser(IActivationMsg activationMsg, IServer server);
	}
}

