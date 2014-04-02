using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Resources;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using MessageTypes.ReplyMessages;
using System.Threading;
using System.Threading.Tasks;

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
        private int _port;

        //Constructor which take ip and port belonging to the targeting server
        public Client(int port)
        {
            Init(port);
        }

        //Init method setting the up the client 
        private void Init(int port)
        {
            clientSocket = new TcpClient();
            _port = port;
            bFormatter = new BinaryFormatter();
        }

        //Method for sending classobject
        public void SendToServer(ISerializable objekt)
        {
            clientSocket = new TcpClient();
            clientSocket.Connect("10.20.32.212", _port);
            outInStream = clientSocket.GetStream();

            bFormatter.Serialize(outInStream, objekt);
        }

        public IReplyMessage ReceiveMessage()
        {
            IReplyMessage reply = (IReplyMessage)bFormatter.Deserialize(outInStream);
            
            outInStream.Close();
            clientSocket.Close();

            return reply;
        }

        // Methode that receives files
        public void ReceiveFile(long fileSize, string fileName)
        {
            int n;
            var rest = fileSize;
            var name = fileName.Substring(fileName.LastIndexOf("\\")+1);
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
