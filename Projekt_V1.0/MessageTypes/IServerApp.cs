using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace Server
{
	//Interface which define which methodes we need to use on the server
	public interface IServerApp
	{
		void VerifyLogin(string email, string password);
		void CreateUser(string email, string password, string firstName, string lastName, string phoneNumber);
	    void CreateJob(string material, bool hollow, string date, string fileName, string email, string comment);
	    void RequestJobs();
	    void DownloadJob(string fileName);
	    void GetMaterials();
	    void ActivationCodeRequest(string email);
	}
}

