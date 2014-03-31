﻿using System;
using System.IO;
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
        IMessage SendToClient(ISerializable message);
        IMessage ServerRun();
        void RecieveFile(string fileName, long fileSize);
        void SendFile(string fileName, long fileSize);
    }

	//Server has interface IServer
	public class Server : IServer
	{
		//Memberdata
		public TcpListener serverSocket { get; private set; }
		public TcpClient clientSocket { get; private set; }
		public NetworkStream inStream { get; private set; }
		public BinaryFormatter bFormatter { get; private set; }
	    private const int maxPacketSize = 1000;

	    //Constructor which takes which port to listening on
		public Server (int port)
		{
			Init (port);
		}

		//Init method which set up server to listening on the ip and port number giving in argument
		private void Init(int port)
		{

		    try
		    {
                Console.WriteLine("test");
                serverSocket = new TcpListener(IPAddress.Parse("10.0.0.1"), port);
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

	    public IMessage SendToClient(ISerializable message)
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

            return (IMessage)bFormatter.Deserialize(inStream);
	    }

	    public void RecieveFile(string fileName, long fileSize)
	    {
	        FileStream fs = new FileStream(fileName, FileMode.CreateNew, FileAccess.Write);
            byte[] buffer = new byte[maxPacketSize];
	        while (fileSize > maxPacketSize)
	        {
	            int i = 0;
	            while (i < maxPacketSize)
	            {
	                i += inStream.Read(buffer, i, 1000-i);
	            }
                fs.Write(buffer, 0, 1000);
                
	            fileSize -= maxPacketSize;
	        }

            int j = 0;
            while (j < fileSize)
            {
                j += inStream.Read(buffer, j, (int)fileSize - j);
            }
            fs.Write(buffer, 0, (int)fileSize);
	    }

        public void SendFile(string fileName, long fileSize)
        {
            byte[] buffer = File.ReadAllBytes("/Jobs/" + fileName);

            int i = 0;
            while (fileSize < maxPacketSize)
            {
                inStream.Write(buffer, i, maxPacketSize);

                i += maxPacketSize;
                fileSize -= maxPacketSize;
            }
            inStream.Write(buffer, i, (int)fileSize);

        }
	}

    public class ServerException : Exception
    {
        public ServerException()
        {
            
        }
    }
}

