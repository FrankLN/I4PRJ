using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApplication1
{
    public class Program
    {
        static void Main(string[] args)
        {
            Message message = new Message();
            BinaryFormatter bFormatter = new BinaryFormatter();
            FileStream fs;

            fs = File.Open("test", FileMode.Create);

            message.AddUser(new User(new Name("Bent", " Hansen")));
            message.AddUser(new User(new Name("Anders", "Andersen")));


            bFormatter.Serialize(fs, message);
            fs.Close();

            message = null;

            fs = File.Open("test", FileMode.Open);
            message = (Message) bFormatter.Deserialize(fs);
            message.PrintMessage();

        }
    }
}
