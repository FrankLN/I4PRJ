using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using MessageTypes;
using MessageTypes.Messages;


namespace ServerApplication
{
    public class Server : IServer
    {
        private TcpListener _serverSocket;
        private TcpClient _clientSocket;
        private NetworkStream _inStream;
        private BinaryFormatter _bFormatter;

        public Server(TcpListener serverSocket, TcpClient clientSocket)
        {
            Init(serverSocket, clientSocket);
        }

        private void Init(TcpListener serverSocket, TcpClient clientSocket)
        {
            _serverSocket = serverSocket;
            _clientSocket = clientSocket;
            _inStream = clientSocket.GetStream();
            _bFormatter = new BinaryFormatter();
        }

        public IMessage RecieveMessage()
        {
            return (IMessage) _bFormatter.Deserialize(_inStream);
        }

        public void SendToClient(ISerializable message)
        {
            _bFormatter.Serialize(_inStream, message);
        }

        public void RecieveFile(string fileName, long fileSize)
        {
            throw new System.NotImplementedException();
        }

        public void SendFile(string fileName, long fileSize)
        {
            throw new System.NotImplementedException();
        }
    }
}
