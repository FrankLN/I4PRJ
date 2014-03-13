using System;
using MessageTypes.Messages;
using MessageTypes.ReplyMessages;

namespace Server
{
    class ServerApp : IServerApp
    {
        private IServer _server;
        //private IDatabase _database;

        public ServerApp(string ip, int port)
        {
            _server = new Server(ip, port);

            while (true)
            {
                _server.ServerRun().Run(this);
            }
            
        }


        public void VerifyLogin(ILoginMsg loginMsg)
        {
            Console.WriteLine("VerifyLogin\n{0}\n{1}", loginMsg.Email, loginMsg.Password);

            LoginReplyMsg loginReplyMsg = new LoginReplyMsg();
            _server.SendToClient(loginReplyMsg);
        }

        public void CreateUser(ICreateUserMsg createUserMsg)
        {
            throw new NotImplementedException();
        }

        public void CreateJob(ICreateJobMsg createJobMsg)
        {
            throw new NotImplementedException();
        }

        public void RequestJobs(IRequestJobsMsg requestJobsMsg)
        {
            throw new NotImplementedException();
        }

        public void DownloadJob(IDownloadJobMsg downloadJobMsg)
        {
            throw new NotImplementedException();
        }

        public void GetMaterials(IGetMaterialsMsg getMaterialsMsg)
        {
            throw new NotImplementedException();
        }

        public void ActivationCodeRequest(IActivationCodeRequestMsg activationCodeRequestMsg)
        {
            throw new NotImplementedException();
        }
    }
}
