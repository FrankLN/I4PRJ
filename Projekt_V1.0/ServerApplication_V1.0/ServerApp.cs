using System;
using System.Linq;
using DatabaseInterface;
using MessageTypes.Messages;
using MessageTypes.ReplyMessages;

namespace Server
{
    class ServerApp : IServerApp
    {
        private IServer _server;
        private IDatabase _database;

        private string GenerateActivationCode()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 8)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());

            return result;
        }

        private void SendEmail(string email, string subject, string text, string server)
        {
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            message.To.Add(email);
            message.Subject = subject;
            message.From = new System.Net.Mail.MailAddress("automail@cartesius.dk");
            message.Body = text;
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(server);
            smtp.Send(message);
        }

        private void SendFile()
        {
            
        }

        public ServerApp(int port)
        {
            _server = new Server(port);

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
                    loginReplyMsg.User = user;
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
            
            _database.AddUser(createUserMsg.User);
            createUserReplyMsg.Created = true;
            createUserReplyMsg.ActivationCode = GenerateActivationCode();

            //send email
            string emailText = "Hello " + createUserMsg.User.FirstName + 
                "\n\nThanks for your reg. on 3D-Printer. To succesfully " +
                "activate your account copy the activation code below and " +
                "paste it in the application. \n\nApplication code: " +
                createUserReplyMsg.ActivationCode + "\n\nThis email cannot be replied.";

            SendEmail(createUserMsg.User.Email, "Activation code to 3D-Printer", 
                                                    emailText, "smtp.google.com");

            //reply
            _server.SendToClient(createUserReplyMsg);
        }

        public void CreateJob(ICreateJobMsg createJobMsg)
        {
            CreateJobReplyMsg createJobReplyMsg = new CreateJobReplyMsg();

            _database.AddJob(createJobMsg.Job);
            createJobReplyMsg.Created = true;

            _server.SendToClient(createJobReplyMsg);
        }

        public void RequestJobs(IRequestJobsMsg requestJobsMsg)
        {
            RequestJobsReplyMsg requestJobsReplyMsg = new RequestJobsReplyMsg();

            requestJobsReplyMsg.JobList = _database.GetJobList();

            _server.SendToClient(requestJobsReplyMsg);
        }

        public void DownloadJob(IDownloadJobMsg downloadJobMsg)
        {
            DownloadJobReplyMsg downloadJobReplyMsg = new DownloadJobReplyMsg();

            _server.SendToClient(downloadJobReplyMsg);

            SendFile();
        }

        public void GetMaterials(IGetMaterialsMsg getMaterialsMsg)
        {
            GetMaterialsReplyMsg getMaterialsReplyMsg = new GetMaterialsReplyMsg();

            //getMaterialsReplyMsg.Materials = _database.GetMaterials();

            _server.SendToClient(getMaterialsReplyMsg);
        }

        public void ActivationCodeRequest(IActivationCodeRequestMsg activationCodeRequestMsg)
        {
            ActivationCodeRequestReplyMsg activationCodeRequestReplyMsg = new ActivationCodeRequestReplyMsg();



            if (_database.GetUserInfo(activationCodeRequestMsg.Email) != null)
            {

                activationCodeRequestReplyMsg.Accepted = true;
                activationCodeRequestReplyMsg.ActivationCode = GenerateActivationCode();

                //send email
                string emailText = "Hello " + _database.GetUserInfo(activationCodeRequestMsg.Email).FirstName +
                                   "\n\nThanks for your reg. on 3D-Printer. To succesfully " +
                                   "activate your account copy the activation code below and " +
                                   "paste it in the application. \n\nApplication code: " +
                                   activationCodeRequestReplyMsg.ActivationCode + "\n\nThis email cannot be replied.";

                SendEmail(activationCodeRequestMsg.Email, "Activation code to 3D-Printer",
                    emailText, "smtp.google.com");
            }
            else
            {
                activationCodeRequestReplyMsg.Accepted = false;

            }

            _server.SendToClient(activationCodeRequestReplyMsg);
        }
    }
}
