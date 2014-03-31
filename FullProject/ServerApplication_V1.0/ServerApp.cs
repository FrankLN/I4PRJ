using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using DatabaseInterface;
using MessageTypes.Messages;
using MessageTypes.ReplyMessages;

namespace Server
{
    class ServerApp : IServerApp
    {
        private IServer _server;
        private IDatabase _database;

        #region helpFunktions
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

        private void SendEmail(string email, string subject, string text)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("3D.Cartesius@gmail.com", "I4PRJGruppe4"),
                EnableSsl = true
            };
            client.Send(email, email, subject, text);
        }
        #endregion

        public ServerApp(int port)
        {
            _server = new Server(port);

            _database = new Database();

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

            if (user.Email == loginMsg.Email)
            {
                loginReplyMsg.Email = true;
                if (user.Password == loginMsg.Password)
                {
                    loginReplyMsg.Password = true;
                    loginReplyMsg.User = user;
                }
                else
                {
                    loginReplyMsg.Password = false;
                }
            }
            else
            {
                loginReplyMsg.Email = false;
                loginReplyMsg.Password = false;
            }
            
            _server.SendToClient(loginReplyMsg).Run(this);
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
                                                    emailText);

            //reply
            _server.SendToClient(createUserReplyMsg).Run(this);
        }

        public void CreateJob(ICreateJobMsg createJobMsg)
        {
            CreateJobReplyMsg createJobReplyMsg = new CreateJobReplyMsg();

            _server.RecieveFile(@"C:\Jobs" + createJobMsg.Job.File, createJobMsg.Job.FileSize);

            _database.AddJob(createJobMsg.Job);
            createJobReplyMsg.Created = true;

            _server.SendToClient(createJobReplyMsg).Run(this);
        }

        public void RequestJobs(IRequestJobsMsg requestJobsMsg)
        {
            RequestJobsReplyMsg requestJobsReplyMsg = new RequestJobsReplyMsg();

            requestJobsReplyMsg.JobList = _database.GetJobList();

            _server.SendToClient(requestJobsReplyMsg).Run(this);
        }

        public void DownloadJob(IDownloadJobMsg downloadJobMsg)
        {
            DownloadJobReplyMsg downloadJobReplyMsg = new DownloadJobReplyMsg();

            if (File.Exists("C:/Jobs/" + downloadJobMsg.FileName))
            {
                downloadJobReplyMsg.FileSize = File.ReadAllBytes("C:/Jobs/" + downloadJobMsg.FileName).Length;

                _server.SendToClient(downloadJobReplyMsg);

                _server.SendFile(downloadJobMsg.FileName, downloadJobReplyMsg.FileSize); 
            }
            else
            {
                downloadJobReplyMsg.FileSize = 0;
            }
            
        }

        public void GetMaterials(IGetMaterialsMsg getMaterialsMsg)
        {
            GetMaterialsReplyMsg getMaterialsReplyMsg = new GetMaterialsReplyMsg();

            Console.WriteLine("Get materials called");

            getMaterialsReplyMsg.Materials = _database.GetMaterials();

            _server.SendToClient(getMaterialsReplyMsg).Run(this);
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
                    emailText);
            }
            else
            {
                activationCodeRequestReplyMsg.Accepted = false;

            }

            _server.SendToClient(activationCodeRequestReplyMsg).Run(this);
        }
    }
}
