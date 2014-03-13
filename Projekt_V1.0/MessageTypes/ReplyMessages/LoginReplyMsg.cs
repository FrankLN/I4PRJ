using System.Runtime.Serialization;
using Server;

namespace MessageTypes.ReplyMessages
{
    public class LoginReplyMsg : IReplyMessage, ISerializable
    {
        public bool Email { get; set; }
        public bool Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public LoginReplyMsg()
        {
            
        }

        public LoginReplyMsg(SerializationInfo info, StreamingContext context)
        {
            Email = (bool)info.GetValue("email", typeof(bool));
            Password = (bool) info.GetValue("password", typeof (bool));
            FirstName = (string)info.GetValue("FirstName", typeof(string));
            LastName = (string)info.GetValue("LastName", typeof(string));
            PhoneNumber = (string)info.GetValue("PhoneNumber", typeof(string));
        }

        public void Run(IServerApp serverApp)
        {
            
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("email", Email);
            info.AddValue("password", Password);
        }
    }
}