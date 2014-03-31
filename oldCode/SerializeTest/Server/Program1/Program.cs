using System;
using MessageTypes;

namespace Program1
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			//Demo...
			Server server = new Server (9000);
			server.Run ();
		}
	}
}
