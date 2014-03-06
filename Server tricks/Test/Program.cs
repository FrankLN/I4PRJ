using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            ProtoAction protoAction = new ProtoAction();
            protoAction.Email = "Test@Test.dk";
            protoAction.Password = "123456";

            TcpClient client = new TcpClient();
            client.Connect("87.48.38.25", 9000);

            Console.WriteLine(" >>> Connected");

            NetworkStream outStream = client.GetStream();

            LIB.writeTextTCP(outStream, "Hemmelig besked");

            Console.WriteLine(" >>> Message recieved: {0}", LIB.readTextTCP(outStream));

            
        }
    }
}
