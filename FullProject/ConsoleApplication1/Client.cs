using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Resources;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using MessageTypes.ReplyMessages;

namespace ClientApplication
{
    public interface IClient
    {
        void SendToServer(ISerializable message);
        IReplyMessage ReceiveMessage();
        void ReceiveFile(long fileSize,string fileName);
        void SendFile(long fileSize, string path);

    }
    public class Client : IClient
    {
        //Memberdata...
        private TcpClient clientSocket;
        private NetworkStream outInStream;
        private BinaryFormatter bFormatter;
        private const int MaxPackageSize = 1000;

        //Constructor which take ip and port belonging to the targeting server
        public Client(int port)
        {
            Init(port);
        }

        //Init method setting the up the client 
        private void Init(int port)
        {
            clientSocket = new TcpClient();
            clientSocket.Connect("10.192.25.182", port);
            outInStream = clientSocket.GetStream();
            bFormatter = new BinaryFormatter();
        }

        //Method for sending classobject
        public void SendToServer(ISerializable objekt)
        {
            bFormatter.Serialize(outInStream, objekt);

        }

        public IReplyMessage ReceiveMessage()
        {
            IReplyMessage reply = (IReplyMessage)bFormatter.Deserialize(outInStream);
            return reply;
        }

        // Methode that receives files
        public void ReceiveFile(long fileSize, string fileName)
        {
            int n;
            var rest = fileSize;
            var name = fileName.Substring(fileName.LastIndexOf("/"));
            var pathAndName = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)+ "Downloads" + name;
                
            var downloadFile = new FileStream(pathAndName,FileMode.Create,FileAccess.Write);

            var receiveBuffer = new byte[MaxPackageSize];
            while (rest != 0)
            {
                n = outInStream.Read(receiveBuffer, 0, MaxPackageSize);
                downloadFile.Write(receiveBuffer, 0, MaxPackageSize);

                rest -= n;
            }
        }
        // Methode that sends a file of a job
        public void SendFile(long fileSize ,string path)
        {
            int n;
            var rest = fileSize;
            var sendBuffer = new byte[MaxPackageSize];
            var uploadFile = new FileStream(path,FileMode.Open);
            while (rest != 0)
            {
                n = uploadFile.Read(sendBuffer, 0, MaxPackageSize);
                outInStream.Write(sendBuffer, 0, n);
                rest -= n;
            }
        }
    }
}
