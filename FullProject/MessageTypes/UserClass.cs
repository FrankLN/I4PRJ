using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseInterface
{
    [Serializable()]
    public class UserClass : ISerializable
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public int AdminRights { get; set; }
        public int Activated { get; set; }

        public UserClass()
        {
            Email = "";
            FirstName = "";
            LastName = "";
            PhoneNumber = "";
            Password = "";
            AdminRights = 0;
            Activated = 0;
        }

        public UserClass(SerializationInfo info, StreamingContext context)
        {
            Email = (string)info.GetValue("Email", typeof(string));
            Password = (string)info.GetValue("Password", typeof(string));
            PhoneNumber = (string)info.GetValue("PhoneNumber", typeof(string));
            FirstName = (string)info.GetValue("FirstName", typeof(string));
            LastName = (string)info.GetValue("LastName", typeof(string));
            AdminRights = (int) info.GetValue("AdminRights", typeof (int));
            Activated = (int)info.GetValue("Activated", typeof(int));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Email", Email);
            info.AddValue("Password", Password);
            info.AddValue("PhoneNumber", PhoneNumber);
            info.AddValue("FirstName", FirstName);
            info.AddValue("LastName", LastName);
            info.AddValue("AdminRights", AdminRights);
            info.AddValue("Activated", Activated);
        }

        
    }
}
