using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using MessageTypes.ReplyMessages;

namespace ClientApplication
{
    /// <summary>
    /// <c>IClient</c> is the interface that holdes the connection functions ´to communicate with the <c>Server</c>
    /// </summary>
    public interface IClient
    {
        //Method for sending classobject
        /// <summary>
        /// <c>SendToServer</c> is the methode that sends serializable objects to the <c>Server</c>.
        /// </summary>
        /// <param name="objekt"> The objekt that is to be send to the <c>Server</c></param>
        void SendToServer(ISerializable message);

        /// <summary>
        /// <c>ReceiveMessage</c> is the methode that receives an objects from the <c>Server</c>.
        /// </summary>
        /// <returns>The message recieved from the <c>Server</c></returns>
        /// 
        IReplyMessage ReceiveMessage();

        /// <summary>
        /// <c>ReceiveFile</c> is the methode that receives the file from the <c>Server</c>.
        /// </summary>
        /// <param name="fileSize">The size of the file that is to be received from the <c>Server</c></param>
        /// <param name="fileName">The name of the file that is to be received from the <c>Server</c></param>
        void ReceiveFile(long fileSize,string fileName);

        /// <summary>
        /// <c>SendFile</c> is the methode that sendes the file to the <c>Server</c>.
        /// </summary>
        /// <param name="fileSize">The size of the file that is to be send to the <c>Server</c></param>
        /// <param name="path">The name of the file that is to be send to the <c>Server</c></param>
        void SendFile(long fileSize, string path);

    }
    /// <summary>
    /// <c>Client</c> is the class, which is responsible for the tcp connection to the <c>Server</c>.
    /// The <c>Client</c> sends messages to the <c>Server</c> to make different requests.
    /// </summary>
    public class Client : IClient
    {
        //Memberdata...
        private TcpClient clientSocket;
        private NetworkStream outInStream;
        private BinaryFormatter bFormatter;
        private const int MaxPackageSize = 1000;
        private int _port;
        private string _ip;

        //Constructor which take ip and port belonging to the targeting server
        /// <summary>
        /// The <c>Client</c>'s constructor. The constructor sets <c>ip</c>
        /// This ip might might change... 
        /// </summary>
        /// <param name="port">The <c>port</c> of the server side</param>
        public Client(int port)
        {
            _port = port;
            // Change this in accordens with the server IHA VPN ip
            _ip = "10.20.32.215";
            clientSocket = new TcpClient();
            bFormatter = new BinaryFormatter();
        }

        //Constructor which take ip and port belonging to the targeting server
        /// <summary>
        /// The <c>Client</c>'s constructor.
        /// </summary>
        /// <param name="port">The <c>port</c> of the server side</param>
        /// <param name="ip">The <c>ip</c> of the server side</param>
        public Client(int port, string ip)
        {
            _port = port;
            _ip = ip;
            clientSocket = new TcpClient();
            bFormatter = new BinaryFormatter();
        }

        //Method for sending classobject
        /// <summary>
        /// <c>SendToServer</c> is the methode that sends serializable objects to the <c>Server</c>.
        /// This is done via the <c>clientSocket</c>.
        /// </summary>
        /// <param name="objekt"> The objekt that is to be send to the <c>Server</c></param>

        public void SendToServer(ISerializable objekt)
        {
            clientSocket = new TcpClient();
            clientSocket.Connect(_ip, _port);
            outInStream = clientSocket.GetStream();
            bFormatter.Serialize(outInStream, objekt);
        }

        /// <summary>
        /// <c>ReceiveMessage</c> is the methode that receives serializable objects from the <c>Server</c>.
        /// <c>ReceiveMessage</c> formattes the incomming stream to an <c>IReplyMessage</c>.
        /// </summary>
        /// <returns>The message recieved from the <c>Server</c></returns>
        public IReplyMessage ReceiveMessage()
        {
            IReplyMessage reply = (IReplyMessage)bFormatter.Deserialize(outInStream);

            if (reply.GetType() == new DownloadJobReplyMsg().GetType())
            {

                ReceiveFile(((DownloadJobReplyMsg)reply).Job.FileSize, ((DownloadJobReplyMsg)reply).Job.File);
            }

            outInStream.Close();
            clientSocket.Close();

            return reply;
        }

        // Methode that receives files
        /// <summary>
        /// <c>ReceiveFile</c> is the methode that receives the file from the <c>Server</c>.
        /// The file is saved in the Download folder.
        /// <c>ReceiveMessage</c> formattes the incomming stream to an <c>IReplyMessage</c>.
        /// </summary>
        /// <param name="fileSize">The size of the file that is to be received from the <c>Server</c></param>
        /// <param name="fileName">The name of the file that is to be received from the <c>Server</c></param>
        public void ReceiveFile(long fileSize, string fileName)
        {
            int n;
            var rest = fileSize;
            var name = fileName.Substring(fileName.LastIndexOf("\\")+1);
            var pathAndName = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)+ "\\Downloads\\" + name;
            
            int i = 1;
            while(File.Exists(pathAndName))
            {
                string fileType = pathAndName.Substring(pathAndName.LastIndexOf("."));
                pathAndName = pathAndName.Substring(0, pathAndName.LastIndexOf("."));
                if (i == 1)
                {
                    pathAndName += "-" + i++;
                }
                else
                {
                    pathAndName = pathAndName.Substring(0, pathAndName.LastIndexOf("-")+1);
                    pathAndName += i++;
                }
                pathAndName += fileType;
            }

            var downloadFile = new FileStream(pathAndName,FileMode.Create,FileAccess.Write);

            var receiveBuffer = new byte[MaxPackageSize];
            while (rest != 0)
            {
                n = outInStream.Read(receiveBuffer, 0, MaxPackageSize);
                downloadFile.Write(receiveBuffer, 0, MaxPackageSize);

                rest -= n;
            }

            downloadFile.Close();
        }
        // Methode that sends a file of a job
        /// <summary>
        /// <c>SendFile</c> is the methode that sendes the file to the <c>Server</c>.
        /// <c>SendFile</c> sendes the file via the <c>NetworkStream</c>.
        /// </summary>
        /// <param name="fileSize">The size of the file that is to be send to the <c>Server</c></param>
        /// <param name="path">The name of the file that is to be send to the <c>Server</c></param>
        public void SendFile(long fileSize ,string path)
        {
            byte[] buffer = File.ReadAllBytes(path);

            int i = 0;
            while (fileSize < MaxPackageSize)
            {
                outInStream.Write(buffer, i, MaxPackageSize);

                i += MaxPackageSize;
                fileSize -= MaxPackageSize;
            }
            outInStream.Write(buffer, i, (int)fileSize);
        }
    }
}
