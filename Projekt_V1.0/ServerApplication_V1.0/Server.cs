using System;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using MessageTypes.Messages;

namespace Server
{
    public interface IServer
    {
        void SendToClient(ISerializable message);
        IMessage ServerRun();
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
		public Server (int port)
		{
			Init (port);
		}

		//Init method which set up server to listening on the ip and port number giving in argument
		private void Init(int port)
		{

		    try
		    {
		        IPAddress ipAddress = Dns.GetHostEntry("localhost").AddressList[0];
		        serverSocket = new TcpListener(ipAddress, port);
		    }
		    catch (SocketException e)
		    {
		        Console.WriteLine(e.Message);
		        throw new ServerException();
		    }
		    catch (ArgumentOutOfRangeException e)
		    {
		        Console.WriteLine(e.Message);
		        throw new ServerException();
		    }
		    
			bFormatter = new BinaryFormatter ();
		}

		//Run method which starts the server
	    public IMessage ServerRun()
	    {
	        try
	        {
	            serverSocket.Start();
                Console.WriteLine("Server running...");
	            clientSocket = serverSocket.AcceptTcpClient();
	            inStream = clientSocket.GetStream();

	        }
	        catch (SocketException e)
	        {
	            Console.WriteLine(e.Message);
	            throw new ServerException();
	        }
	        catch (ObjectDisposedException e)
	        {
	            Console.WriteLine(e.Message);
	            throw new ServerException();
	        }
	        catch (InvalidOperationException e)
	        {
	            Console.WriteLine(e.Message);
	            throw new ServerException();
	        }

	        //Deserialize and then return to server
	        return (IMessage) bFormatter.Deserialize(inStream);
	    }

	    public void SendToClient(ISerializable message)
	    {
	        try
	        {
	            bFormatter.Serialize(inStream, message);
	        }
	        catch (ArgumentNullException e)
	        {
	            Console.WriteLine(e.Message);
	            throw new ServerException();
	        }
	        catch (SerializationException e)
	        {
	            Console.WriteLine(e.Message);
	            throw new ServerException();
	        }
	        catch (SecurityException e)
	        {
	            Console.WriteLine(e.Message);
                throw new ServerException();
	        }
	        
	    }
	}

    public class ServerException : Exception
    {
        public ServerException()
        {
            
        }
    }
}

