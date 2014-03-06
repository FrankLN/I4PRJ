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
            Connect();
        }

        private void Connect()
        {
            client = new TcpClient();
            client.Connect("87.48.38.25", 9000);

            Console.WriteLine(" >>> Connected");

            outStream = client.GetStream();
        }

        public void SendMessage(string str)
        {
            LIB.writeTextTCP(outStream, "Hej");

        }

        public void SendClass(ISerializable objekt)
        {
            BinaryFormatter bFormatter = new BinaryFormatter();
            
            bFormatter.Serialize(outStream, objekt);
        }

        public string ReadMessage()
        {
            return LIB.readTextTCP(outStream);
        }

        public ISerializable ReadClass()
        {
            BinaryFormatter bFormatter = new BinaryFormatter();

            return (ISerializable) bFormatter.Deserialize(outStream);
        }
    }
}
