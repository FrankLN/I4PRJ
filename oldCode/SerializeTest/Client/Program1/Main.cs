using System;
using MessageTypes;


namespace Program1
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			//A demo...
			Client client = new Client("10.0.0.1", 9000);
			ProtoType test = new ProtoType ();

			test.Test = "Username";
			test.Test2 = "Password";

			client.SendClass(test);
		}

	}
}
