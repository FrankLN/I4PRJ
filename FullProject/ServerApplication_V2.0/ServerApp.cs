using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Threading;
using DatabaseInterface;
using MessageTypes;
using MessageTypes.Messages;
using MessageTypes.ReplyMessages;

namespace ServerApplication
{
    /// <summary>
    /// ServerApp is the system controller on the server. 
    /// Its primary task is to handle and reply incomming messages.
    /// </summary>
    public class ServerApp : IServerApp
    {
        /// <summary>
        /// The port number to listen on. It is protected for test purposes.
        /// </summary>
        protected int _port;

        /// <summary>
        /// The database interface. It is protected for test purposes.
        /// </summary>
        protected IDatabase _database;

        #region helpFunktions

        /// <summary>
        /// <c>GenerateActivationCode</c> is a methode that generate a random 8 numbers or letters long code.
        /// </summary>
        /// <returns>A string with 8 numbers and letters</returns>
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

        /// <summary>
        /// <c>SendEmail</c> is a methode that sends an email.
        /// </summary>
        /// <param name="email">The reciever email.</param>
        /// 
        /// <param name="subject">The subject to the email.</param>
        /// 
        /// <param name="text">The text of the email.</param>
        /// 
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

        /// <summary>
        /// The <c>ServerApp</c> constructor.
        /// </summary>
        /// <param name="port">The port which the ServerApp should listen to.</param>
        /// 
        /// <param name="database">The database interface the ServerApp use to handle data.</param>
        /// 
        public ServerApp(int port, IDatabase database)
        {
            _port = port;
            _database = database;
        }

        /// <summary>
        /// <c>RunServerApp</c> is the methode where <c>Client</c>'s get connected to the server.
        /// After connection the handling of the recieved message gets proceeded in a thread,
        /// to allow more <c>Client</c> requests.
        /// </summary>
        public void RunServerApp()
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

        /// <summary>
        /// <c>RecieveMessage</c> is the methode for handling messages.
        /// A new instance of <c>Server</c> is created
        /// </summary>
        /// <param name="stateInfo">Contains a TcpListener and a TcpClient</param>
        private void ReciveMessage(Object stateInfo)
        {
            object[] array = stateInfo as object[];
            IServer server = new Server((TcpListener)array[0], (TcpClient)array[1]);

            server.RecieveMessage().Run(this, server);
        }

        #region Message handling

        /// <summary>
        /// <c>VerifyLogin</c> is the methode for handling login requests.
        /// </summary>
        /// <param name="loginMsg">The message recieved from the <c>Client</c>.</param>
        /// <param name="server">The <c>Server</c> instance to reply with.</param>
        public void VerifyLogin(ILoginMsg loginMsg, IServer server)
        {
            LoginReplyMsg loginReplyMsg = new LoginReplyMsg();
            Console.WriteLine("VerifyLogin\n{0}\n{1}", loginMsg.Email, loginMsg.Password);

            UserClass user = _database.GetUserInfo(loginMsg.Email);

            loginReplyMsg.Activated = (user.Activated > 0);

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

        /// <summary>
        /// <c>CreateUser</c> is the methode for handling new user requests.
        /// </summary>
        /// <param name="createUserMsg">The message recieved from the <c>Client</c></param>
        /// <param name="server">The <c>Server</c> instance to reply with.</param>
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

        /// <summary>
        /// <c>CreateJob</c> is the methode for handling new job requests.
        /// </summary>
        /// <param name="createJobMsg">The message recieved from the <c>Client</c></param>
        /// <param name="server">The <c>Server</c> instance to reply with.</param>
        public void CreateJob(ICreateJobMsg createJobMsg, IServer server)
        {
            CreateJobReplyMsg createJobReplyMsg = new CreateJobReplyMsg();

            Console.WriteLine(createJobMsg.Job.File);
            Console.WriteLine(createJobMsg.Job.FileSize);

            createJobMsg.Job.CreationTime = "Now";
            createJobMsg.Job.File = createJobMsg.Job.File.Substring(createJobMsg.Job.File.LastIndexOf("\\")+1);

            _database.AddJob(createJobMsg.Job);

            Console.WriteLine(createJobMsg.Job.File);

            server.RecieveFile("C:\\Jobs\\" + createJobMsg.Job.OrderId + "\\" + createJobMsg.Job.File, createJobMsg.Job.FileSize);

            createJobReplyMsg.Created = true;

            server.SendToClient(createJobReplyMsg);
        }

        /// <summary>
        /// <c>RequestJobs</c> is the methode for handling list of jobs requests.
        /// </summary>
        /// <param name="requestJobsMsg">The message recieved from the <c>Client</c></param> 
        /// <param name="server">The <c>Server</c> instance to reply with.</param>
        public void RequestJobs(IRequestJobsMsg requestJobsMsg, IServer server)
        {
            RequestJobsReplyMsg requestJobsReplyMsg = new RequestJobsReplyMsg();

            requestJobsReplyMsg.JobList = _database.GetJobList();

            server.SendToClient(requestJobsReplyMsg);
        }

        /// <summary>
        /// <c>DownloadJob</c> is the methode for handling download job requests.
        /// </summary>
        /// <param name="downloadJobMsg">The message recieved from the <c>Client</c></param>
        /// <param name="server">The <c>Server</c> instance to reply with.</param>
        public void DownloadJob(IDownloadJobMsg downloadJobMsg, IServer server)
        {
            DownloadJobReplyMsg downloadJobReplyMsg = new DownloadJobReplyMsg();

            

            if (File.Exists("C:\\Jobs\\" + downloadJobMsg.Job.OrderId + "\\" + downloadJobMsg.Job.File))
            {
                downloadJobReplyMsg.Job.FileSize = File.ReadAllBytes("C:\\Jobs\\" + downloadJobMsg.Job.OrderId + "\\" + downloadJobMsg.Job.File).Length;
                downloadJobReplyMsg.Job.File = downloadJobMsg.Job.File;
                server.SendToClient(downloadJobReplyMsg);

                server.SendFile("C:\\Jobs\\" + downloadJobMsg.Job.OrderId + "\\" + downloadJobMsg.Job.File, downloadJobReplyMsg.Job.FileSize);
            }
            else
            {
                downloadJobReplyMsg.Job.FileSize = 0;
            }
        }

        /// <summary>
        /// <c>GetMaterials</c> is the methode for handling list of material requests.
        /// </summary>
        /// <param name="getMaterialsMsg">The message recieved from the <c>Client</c></param>
        /// 
        /// <param name="server">The <c>Server</c> instance to reply with.</param>
        /// 
        public void GetMaterials(IGetMaterialsMsg getMaterialsMsg, IServer server)
        {
            GetMaterialsReplyMsg getMaterialsReplyMsg = new GetMaterialsReplyMsg();

            Console.WriteLine("Get meterials called");

            getMaterialsReplyMsg.Materials = _database.GetMaterials();

            server.SendToClient(getMaterialsReplyMsg);
        }

        /// <summary>
        /// <c>ActivationCodeRequest</c> is the methode for handling new activation code request.
        /// </summary>
        /// <param name="activationCodeRequestMsg">The message recieved from the <c>Client</c></param>
        /// 
        /// <param name="server">The <c>Server</c> instance to reply with.</param>
        /// 
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

        /// <summary>
        /// <c>ActivateUser</c> is the methode for handling user activation request.
        /// </summary>
        /// <param name="activationMsg">The message recieved from the <c>Client</c></param>
        /// 
        /// <param name="server">The <c>Server</c> instance to reply with.</param>
        /// 
        public void ActivateUser(IActivationMsg activationMsg, IServer server)
        {
            ActivationReplyMsg activationReplyMsg = new ActivationReplyMsg();

            UserClass user = _database.GetUserInfo(activationMsg.User.Email);

            if (user.ActivationCode == activationMsg.User.ActivationCode)
            {
                user.Activated = 1;
                _database.ActivateUser(user);
                activationReplyMsg.UserActivated = true;
            }
            
            server.SendToClient(activationReplyMsg);
        }

        #endregion
    }
}
