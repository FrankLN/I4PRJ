using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using MessageTypes.ReplyMessages;

namespace ClientApplication
{
    public interface IClient
    {
        IReplyMessage ClientRun();
        void SendToServer(ISerializable message);
    }
    class Client : IClient
    {
        //Memberdata...
        private TcpClient clientSocket;
        private NetworkStream outInStream;
        private BinaryFormatter bFormatter;

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

        public IReplyMessage ClientRun()
        {
            IReplyMessage reply = (IReplyMessage)bFormatter.Deserialize(outInStream);
            return reply;
        }
    }
}
