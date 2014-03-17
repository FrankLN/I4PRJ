using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    [Serializable]
    public class Message : ISerializable
    {
        private List<User> users;
        public Message()
        {
            users = new List<User>();
        }

        public Message(SerializationInfo info, StreamingContext context)
        {
            users = (List<User>) info.GetValue("user", typeof (List<User>));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("user", users);
        }

        public void AddUser(User user)
        {
            users.Add(user);
        }

        public void PrintMessage()
        {
            for(int i = 0; i < users.Count; i++)
                users[i].printUser();
        }
    }
}
