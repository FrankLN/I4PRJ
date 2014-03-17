using System;
using DatabaseInterface;
using MessageTypes.Messages;
using MessageTypes.ReplyMessages;

namespace Server
{
    class ServerApp : IServerApp
    {
        private IServer _server;
        private IDatabase _database;

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
            LoginReplyMsg loginReplyMsg = new LoginReplyMsg();
            Console.WriteLine("VerifyLogin\n{0}\n{1}", loginMsg.Email, loginMsg.Password);

            UserClass user = _database.GetUserInfo(loginMsg.Email);

            if (user != null)
            {
                loginReplyMsg.Email = true;
                if (user.Password == loginMsg.Password)
                {
                    loginReplyMsg.Password = true;
                    loginReplyMsg.FirstName = user.FirstName;
                    loginReplyMsg.LastName = user.LastName;
                    loginReplyMsg.PhoneNumber = user.PhoneNumber;
                }
            }
            else
            {
                loginReplyMsg.Email = false;
                loginReplyMsg.Password = false;
            }
            
            _server.SendToClient(loginReplyMsg);
        }

        public void CreateUser(ICreateUserMsg createUserMsg)
        {
            CreateUserReplyMsg createUserReplyMsg = new CreateUserReplyMsg();
            
            var user = new User();

            user.FirstName = createUserMsg.FirstName;
            user.LastName = createUserMsg.LastName;
            user.PhoneNumber = createUserMsg.PhoneNumber;
            user.Password = createUserMsg.Password;
            user.Email = createUserMsg.Email;

            dbInterface.InsertUser(user);

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
