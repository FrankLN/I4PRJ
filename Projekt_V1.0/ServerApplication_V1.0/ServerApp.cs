using System;
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


        public void VerifyLogin(string email, string password)
        {
            Console.WriteLine("VerifyLogin");

            LoginReplyMsg loginReplyMsg = new LoginReplyMsg();
            _server.SendToClient(loginReplyMsg);
        }

        public void CreateUser(string email, string password, string firstName, string lastName, string phoneNumber = null)
        {
            throw new NotImplementedException();
        }

        public void CreateJob(string material, bool hollow, string date, string fileName, string email, string comment = null)
        {
            throw new NotImplementedException();
        }

        public void RequestJobs()
        {
            throw new NotImplementedException();
        }

        public void DownloadJob(string fileName)
        {
            throw new NotImplementedException();
        }

        public void GetMaterials()
        {
            throw new NotImplementedException();
        }

        public void ActivationCodeRequest(string email)
        {
            throw new NotImplementedException();
        }
    }
}
