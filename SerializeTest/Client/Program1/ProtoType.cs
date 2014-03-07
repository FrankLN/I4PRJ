using System;
using System.Runtime.Serialization;
using Program1;

namespace MessageTypes
{
	//Interface for the server. The server just call run and the magic is happening
	public interface IAction
	{
		void Run(IServerApp control);
	}

	//Serializable prototype. We want one of theese for every message that can be send
	[Serializable()]
	public class ProtoType : ISerializable, IAction
	{
		//Memberdata
		string test, test2;
		public string Test{get { return test; } set { test = value; }}
		public string Test2{get { return test2; } set { test2 = value; }}

		//Default constructor
		public ProtoType ()
		{
		}

		//Serialize constructor, whitout this it wont work
		public ProtoType(SerializationInfo info, StreamingContext context)
		{
			//Every memberdata is filled out like this
			Test = (string)info.GetValue ("test", typeof(string));
			Test2 = (string)info.GetValue ("test2", typeof(string));
			//if we had an int i...
			//i = (int)info.GetValue("i", typeof(int));
		}

		//Serialize method, without this it wont work
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			//Every memberdata is filled out like this. 
			//It is very important the first argument is the same, 
			//as first argument in the info.GetValue method as shown
			info.AddValue ("test", test);
			info.AddValue ("test2", test2);
			//if we had an int i...
			//info.AddValue("i", i);
		}

		//This method Run is called when the class is recieved at the server
		//It has an interface which server will fill with "this"
		public void Run (IServerApp control)
		{
			//Just for testing
			Console.WriteLine (Test);
			Console.WriteLine (Test2);

			//Just for testing
			control.DoThis();
			control.DoThat();
		}
	}
}

