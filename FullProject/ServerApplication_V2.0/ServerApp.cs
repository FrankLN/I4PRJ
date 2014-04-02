using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using DatabaseInterface;
using MessageTypes;
using MessageTypes.Messages;
using MessageTypes.ReplyMessages;
using Server;

namespace ServerApplication
{
    public class ServerApp : IServerApp
    {
        private int _port;
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
            _port = port;
            _database = new Database();

            RunServerApp();
        }

        private void RunServerApp()
        {
            TcpListener serverSocket = new TcpListener(IPAddress.Any, _port);
            TcpClient clientSocket;

            serverSocket.Start();

            Console.WriteLine("Server started...");

            while (true)
            {
                clientSocket = serverSocket.AcceptTcpClient();

                Console.WriteLine("Servicise a Thread...");

                ThreadPool.QueueUserWorkItem(new WaitCallback(ReciveMessage), new object[] {serverSocket, clientSocket});
            }
        }

        private void ReciveMessage(Object stateInfo)
        {
            object[] array = stateInfo as object[];
            IServer server = new Server((TcpListener)array[0], (TcpClient)array[1]);

            server.RecieveMessage().Run(this, server);

        }

        #region Message handling
        public void VerifyLogin(ILoginMsg loginMsg, IServer server)
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

            server.SendToClient(loginReplyMsg);
        }

        public void CreateUser(ICreateUserMsg createUserMsg, IServer server)
        {
            CreateUserReplyMsg createUserReplyMsg = new CreateUserReplyMsg();

            createUserMsg.User.ActivationCode = GenerateActivationCode();
            _database.AddUser(createUserMsg.User);
            createUserReplyMsg.Created = true;

            //send email
            string emailText = "Hello " + createUserMsg.User.FirstName +
                "\n\nThanks for your reg. on 3D-Printer. To succesfully " +
                "activate your account copy the activation code below and " +
                "paste it in the application. \n\nApplication code: " +
                createUserMsg.User.ActivationCode + "\n\nThis email cannot be replied.";

            SendEmail(createUserMsg.User.Email, "Activation code to 3D-Printer",
                                                    emailText);

            //reply
            server.SendToClient(createUserReplyMsg);
        }

        public void CreateJob(ICreateJobMsg createJobMsg, IServer server)
        {
            CreateJobReplyMsg createJobReplyMsg = new CreateJobReplyMsg();

            Console.WriteLine(createJobMsg.Job.File);
            Console.WriteLine(createJobMsg.Job.FileSize);

            createJobMsg.Job.File = createJobMsg.Job.OrderId + "\\" + createJobMsg.Job.File.Substring(createJobMsg.Job.File.LastIndexOf("\\") + 1);

            Console.WriteLine(createJobMsg.Job.File);

            server.RecieveFile(createJobMsg.Job.File, createJobMsg.Job.FileSize);

            createJobMsg.Job.CreationTime = "Now";

            _database.AddJob(createJobMsg.Job);

            Directory.Move("C:\\Jobs\\0", "C:\\Jobs\\" + createJobMsg.Job.OrderId);

            createJobReplyMsg.Created = true;

            server.SendToClient(createJobReplyMsg);
        }

        public void RequestJobs(IRequestJobsMsg requestJobsMsg, IServer server)
        {
            RequestJobsReplyMsg requestJobsReplyMsg = new RequestJobsReplyMsg();

            requestJobsReplyMsg.JobList = _database.GetJobList();

            server.SendToClient(requestJobsReplyMsg);
        }

        public void DownloadJob(IDownloadJobMsg downloadJobMsg, IServer server)
        {
            DownloadJobReplyMsg downloadJobReplyMsg = new DownloadJobReplyMsg();

            if (File.Exists("C:\\Jobs\\" + downloadJobMsg.FileName))
            {
                downloadJobReplyMsg.FileSize = File.ReadAllBytes("C:\\Jobs\\" + downloadJobMsg.FileName).Length;

                server.SendToClient(downloadJobReplyMsg);

                server.SendFile(downloadJobMsg.FileName, downloadJobReplyMsg.FileSize);
            }
            else
            {
                downloadJobReplyMsg.FileSize = 0;
            }
        }

        public void GetMaterials(IGetMaterialsMsg getMaterialsMsg, IServer server)
        {
            GetMaterialsReplyMsg getMaterialsReplyMsg = new GetMaterialsReplyMsg();

            Console.WriteLine("Get meterials called");

            getMaterialsReplyMsg.Materials = _database.GetMaterials();

            server.SendToClient(getMaterialsReplyMsg);
        }

        public void ActivationCodeRequest(IActivationCodeRequestMsg activationCodeRequestMsg, IServer server)
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

            server.SendToClient(activationCodeRequestReplyMsg);
        }
        #endregion

        #region Old_Commands
        public void VerifyLogin(ILoginMsg loginMsg)
        {
            Console.WriteLine("Ikke implementeret");
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
        #endregion
    }
}
