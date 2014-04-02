using System.ComponentModel;
using System.IO;
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
        private const int maxPacketSize = 1000;

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
            Directory.SetCurrentDirectory("C:\\Jobs\\");
            Directory.CreateDirectory(fileName.Substring(0, fileName.LastIndexOf("\\")));



            FileStream fs = File.Open(fileName, FileMode.Create);
            byte[] buffer = new byte[maxPacketSize];
            while (fileSize > maxPacketSize)
            {
                int i = 0;
                while (i < maxPacketSize)
                {
                    i += _inStream.Read(buffer, i, 1000 - i);
                }
                fs.Write(buffer, 0, 1000);

                fileSize -= maxPacketSize;
            }

            int j = 0;
            while (j < fileSize)
            {
                j += _inStream.Read(buffer, j, (int)fileSize - j);
            }
            fs.Write(buffer, 0, (int)fileSize);

            fs.Close();
        }

        public void SendFile(string fileName, long fileSize)
        {
            byte[] buffer = File.ReadAllBytes(fileName);

            int i = 0;
            while (fileSize < maxPacketSize)
            {
                _inStream.Write(buffer, i, maxPacketSize);

                i += maxPacketSize;
                fileSize -= maxPacketSize;
            }
            _inStream.Write(buffer, i, (int)fileSize);

            
        }
    }
}
