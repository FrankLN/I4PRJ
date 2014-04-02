using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using MessageTypes;
using MessageTypes.Messages;
using MessageTypes.ReplyMessages;

namespace Server
{
	//Interface which define which methodes we need to use on the server
	public interface IServerApp
	{
        void VerifyLogin(ILoginMsg loginMsg);
		void CreateUser(ICreateUserMsg createUserMsg);
	    void CreateJob(ICreateJobMsg createJobMsg);
	    void RequestJobs(IRequestJobsMsg requestJobsMsg);
	    void DownloadJob(IDownloadJobMsg downloadJobMsg);
	    void GetMaterials(IGetMaterialsMsg getMaterialsMsg);
	    void ActivationCodeRequest(IActivationCodeRequestMsg activationCodeRequestMsg);
        void ActivateUser(IActivationCodeRequestMsg activationCodeRequestMsg);

        void VerifyLogin(ILoginMsg loginMsg, IServer server);
        void CreateUser(ICreateUserMsg createUserMsg, IServer server);
        void CreateJob(ICreateJobMsg createJobMsg, IServer server);
        void RequestJobs(IRequestJobsMsg requestJobsMsg, IServer server);
        void DownloadJob(IDownloadJobMsg downloadJobMsg, IServer server);
        void GetMaterials(IGetMaterialsMsg getMaterialsMsg, IServer server);
	    void ActivationCodeRequest(IActivationCodeRequestMsg activationCodeRequestMsg, IServer server);
        void ActivateUser(IActivationCodeRequestMsg activationCodeRequestMsg, IServer server);
	}
}

