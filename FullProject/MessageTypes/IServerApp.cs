using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using MessageTypes.Messages;

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
	}
}

