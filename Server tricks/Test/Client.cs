using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Client
    {
        private TcpClient client;
        private NetworkStream outStream;
        public Client()
        {
            client = new TcpClient();
            client.Connect("10.168.0.103", 9000);

            Console.WriteLine(" >>> Connected");

            outStream = client.GetStream();
        }

        public string SendMessage(string str)
        {
            LIB.writeTextTCP(outStream, "Hej");

            return LIB.readTextTCP(outStream);
        }

        public void SendClass(ProtoAction objekt)
        {
            BinaryFormatter bFormatter = new BinaryFormatter();
            
            bFormatter.Serialize(outStream, objekt);

            outStream.Close();
            client.Close();
        }
    }
}
