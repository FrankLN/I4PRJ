using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using MessageTypes.Messages;

namespace Server
{
    public interface IServer
    {
        void SendToClient(ISerializable message);
    }

	//Server has interface IServer
	public class Server : IServer
	{
		//Memberdata
		TcpListener serverSocket;
		TcpClient clientSocket;
		NetworkStream inStream;
		BinaryFormatter bFormatter;

		//Constructor which takes which ip-addres and port to listening on
		public Server (string ip, int port)
		{
			Init (ip, port);
		}

		//Init method which set up server to listening on the ip and port number giving in argument
		private void Init(string ip, int port)
		{
			serverSocket = new TcpListener (IPAddress.Parse(ip), port);
			clientSocket = default(TcpClient);
			bFormatter = new BinaryFormatter ();
		}

		//Run method which starts the server
		public IMessage ServerRun()
		{
			serverSocket.Start ();
			clientSocket = serverSocket.AcceptTcpClient ();
			inStream = clientSocket.GetStream ();
            //Deserialize and then return to server
            return (IMessage)bFormatter.Deserialize(inStream);
		}

	    public void SendToClient(ISerializable message)
	    {
	        bFormatter.Serialize(inStream, message);
	    }
	}
}

