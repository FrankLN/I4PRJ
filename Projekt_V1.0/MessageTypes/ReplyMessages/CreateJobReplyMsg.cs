using System.Runtime.Serialization;
using Server;

namespace MessageTypes.ReplyMessages
{
    public class CreateJobReplyMsg : IReplyMessage, ISerializable
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public CreateJobReplyMsg()
        {

        }

        public CreateJobReplyMsg(SerializationInfo info, StreamingContext context)
        {
            Email = (string)info.GetValue("Email", typeof(string));
            Password = (string)info.GetValue("Password", typeof(string));
            PhoneNumber = (string)info.GetValue("PhoneNumber", typeof(string));
            FirstName = (string)info.GetValue("FirstName", typeof(string));
            LastName = (string)info.GetValue("LastName", typeof(string));
        }

        public void Run(IServerApp serverApp)
        {
            
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Email", Email);
            info.AddValue("Password", Password);
            info.AddValue("PhoneNumber", PhoneNumber);
            info.AddValue("FirsName", FirstName);
            info.AddValue("LastName", LastName);
        }
    }
}