using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net.Sockets;

namespace Program1
{
	public class Client
	{
		//Memberdata...
		private TcpClient clientSocket;
		private NetworkStream outStream;
		private BinaryFormatter bFormatter;

		//Constructor which take ip and port belonging to the targeting server
		public Client (string ip, int port)
		{
			Init(ip, port);
		}

		//Init method setting the up the client 
		private void Init (string ip, int port)
		{
			clientSocket = new TcpClient();
			clientSocket.Connect(ip, port);
			outStream = clientSocket.GetStream();
			bFormatter = new BinaryFormatter();
		}

		//Method for sending classobject
		public void SendClass (ISerializable objekt)
		{
			bFormatter.Serialize(outStream, objekt);
		}
	}
}

