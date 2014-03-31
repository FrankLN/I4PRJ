using System;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using MessageTypes;

namespace Program1
{
	//Server has interface IServerApp so we can call method Run(this) on recived class
	public class Server : IServerApp
	{
		//Memberdata
		TcpListener serverSocket;
		TcpClient clientSocket;
		NetworkStream inStream;
		BinaryFormatter bFormatter;

		//Constructor which takes what port to listening on
		public Server (int port)
		{
			Init (port);
		}

		//Init method which set up server to listening on the port number giving in argument
		private void Init(int port)
		{
			serverSocket = new TcpListener (IPAddress.Parse("10.0.0.1"), port);
			clientSocket = default(TcpClient);
			bFormatter = new BinaryFormatter ();
		}

		//Run method which starts the server
		public void Run()
		{
			serverSocket.Start ();
			clientSocket = serverSocket.AcceptTcpClient ();
			inStream = clientSocket.GetStream ();
			//Deserialize and then run
			((IAction)bFormatter.Deserialize (inStream)).Run (this);
			//call method run and recieve another class
			Run ();
		}


		//Just for test...
		public void DoThis()
		{
			Console.WriteLine("DoThis method called");
		}

		public void DoThat()
		{
			Console.WriteLine("DoThat method called");
		}
	}
}

