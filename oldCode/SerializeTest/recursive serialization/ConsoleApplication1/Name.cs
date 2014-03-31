using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    [Serializable]
    public class Name : ISerializable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Name(string fName, string lName)
        {
            FirstName = fName;
            LastName = lName;
        }

        public Name(SerializationInfo info, StreamingContext context)
        {
            FirstName = (string)info.GetValue("fName", typeof (string));
            LastName = (string) info.GetValue("lName", typeof (string));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("fName", FirstName);
            info.AddValue("lName", LastName);
        }

        public void PrintName()
        {
            Console.WriteLine(FirstName + " " + LastName);
        }
    }
}
