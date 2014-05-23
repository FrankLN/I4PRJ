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
    /// <summary>
    /// The Server package
    /// </summary>
    [System.Runtime.CompilerServices.CompilerGenerated]
    class NamespaceDoc
    {
    }

    /// <summary>
    /// <c>Server</c> is the class, which is responsible for the tcp connection to the <c>Client</c>.
    /// For every <c>Client</c> connected, a <c>Sever</c> instance is created.
    /// This way makes sure that the <c>NetworkStream</c> stays unique.
    /// </summary>
    public class Server : IServer
    {
        private TcpListener _serverSocket;
        private TcpClient _clientSocket;
        private NetworkStream _inStream;
        private BinaryFormatter _bFormatter;
        private const int maxPacketSize = 1000;

        /// <summary>
        /// The <c>Server</c>'s constructor. The constructor calls the methode <c>Init</c>
        /// </summary>
        /// <param name="serverSocket">The <c>serverSocket</c></param>
        /// <param name="clientSocket">The <c>clientSocket</c></param>
        public Server(TcpListener serverSocket, TcpClient clientSocket)
        {
            Init(serverSocket, clientSocket);
        }

        /// <summary>
        /// <c>Init</c> is the methode that initilize the <c>Server</c>'s memberdata.
        /// </summary>
        /// <param name="serverSocket">The serverSocket</param>
        /// <param name="clientSocket">The clientSocket</param>
        private void Init(TcpListener serverSocket, TcpClient clientSocket)
        {
            _serverSocket = serverSocket;
            _clientSocket = clientSocket;
            _inStream = clientSocket.GetStream();
            _bFormatter = new BinaryFormatter();
        }

        /// <summary>
        /// The <c>RecieveMessage</c> methode, recieves a message from the connected client,
        /// Deserialize it,
        /// then cast the return value to the interface <c>IMessage</c>
        /// and at last returns the message to where the methode where called, <c>ServerApp</c>
        /// </summary>
        /// <returns>The message recieved from the server</returns>
        public IMessage RecieveMessage()
        {
            return (IMessage) _bFormatter.Deserialize(_inStream);
        }

        /// <summary>
        /// The <c>SendToClient</c> methode sends a reply to the client.
        /// The message is serialized and sent with the BinaryFormatter.
        /// </summary>
        /// <param name="message">The message send to the client</param>
        public void SendToClient(ISerializable message)
        {
            _bFormatter.Serialize(_inStream, message);
        }

        /// <summary>
        /// The <c>RecieveFile</c> method recieves a file from the connected client,
        /// and saves it in a directory on the server.
        /// A directory for every job is created named after the jobId,
        /// the file is named after the jobName and placed in the job's directory.
        /// </summary>
        /// <param name="fileName">The name of the file, should be in this format: C:\\Jobs\\"JobId"\\"JobFileName"</param>
        /// <param name="fileSize">The size of the file</param>
        public void RecieveFile(string fileName, long fileSize)
        {
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

        /// <summary>
        /// The method <c>SendFile</c> sends files to the connected client.
        /// </summary>
        /// <param name="fileName">The name of the file, should be in this format: C:\\Jobs\\"JobId"\\"JobFileName"</param>
        /// <param name="fileSize">The size of the file</param>
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
