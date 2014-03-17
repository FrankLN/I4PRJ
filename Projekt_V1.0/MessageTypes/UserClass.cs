using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseInterface
{
    [Serializable]
    public class UserClass : ISerializable
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public int AdminRights { get; set; }

        public UserClass()
        {
            
        }

        public UserClass(SerializationInfo info, StreamingContext context)
        {
            Email = (string)info.GetValue("Email", typeof(string));
            Password = (string)info.GetValue("Password", typeof(string));
            PhoneNumber = (string)info.GetValue("PhoneNumber", typeof(string));
            Name = (string)info.GetValue("Name", typeof(string));
            AdminRights = (int) info.GetValue("AdminRight", typeof (int));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Email", Email);
            info.AddValue("Password", Password);
            info.AddValue("PhoneNumber", PhoneNumber);
            info.AddValue("Name", Name);
            info.AddValue("AdminRights", AdminRights);
        }
    }
}
