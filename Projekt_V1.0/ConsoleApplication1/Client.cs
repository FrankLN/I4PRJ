using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using MessageTypes.ReplyMessages;

namespace ClientApplication
{
    public interface IClient
    {
        void SendToServer(ISerializable message);
        IReplyMessage ClientRun();
        
    }
    class Client : IClient
    {
        //Memberdata...
        private TcpClient clientSocket;
        private NetworkStream outInStream;
        private BinaryFormatter bFormatter;
        private int maxPackageSize = 1000;

        //Constructor which take ip and port belonging to the targeting server
        public Client(int port)
        {
            Init(port);
        }

        //Init method setting the up the client 
        private void Init(int port)
        {
            IPAddress ip = Dns.GetHostEntry("localhost").AddressList[0];
            clientSocket = new TcpClient();
            clientSocket.Connect(ip, port);
            outInStream = clientSocket.GetStream();
            bFormatter = new BinaryFormatter();
        }

        //Method for sending classobject
        public void SendToServer(ISerializable objekt)
        {
            bFormatter.Serialize(outInStream, objekt);

        }
        // Methode that receives messages
        public IReplyMessage ClientRun()
        {
            IReplyMessage reply = (IReplyMessage)bFormatter.Deserialize(outInStream);
            return reply;

        }
        // Methode that receives files
        public void ReceiveFromServer(long fileSize, string fileName)
        {
            int n;
            long rest = fileSize;
            string path;
            FileStream downloadFile = new FileStream(path,FileMode.Create,FileAccess.Write);
            
            var receiveBuffer = new byte[1000];
            while (rest != 0)
            {
                n = outInStream.Read(receiveBuffer, 0, maxPackageSize);

                rest -= n;
            }
        }
    }
}
