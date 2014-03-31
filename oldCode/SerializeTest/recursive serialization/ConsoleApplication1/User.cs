using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    [Serializable]
    public class User : ISerializable
    {
        private Name _name;

        public User(Name name)
        {
            _name = name;
        }

        public User(SerializationInfo info, StreamingContext context)
        {
            _name = (Name)info.GetValue("name", typeof(Name));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("name", _name);
        }

        public void printUser()
        {
            _name.PrintName();
        }
    }
}
