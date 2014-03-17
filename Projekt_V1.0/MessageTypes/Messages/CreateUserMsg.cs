using System.Runtime.Serialization;
using DatabaseInterface;
using Server;

namespace MessageTypes.Messages
{
    public interface ICreateUserMsg
    {
        UserClass User { get; }
    }
    public class CreateUserMsg : IMessage, ISerializable, ICreateUserMsg
    {
        public UserClass User { get; set; }

        public CreateUserMsg()
        {

        }

        public CreateUserMsg(SerializationInfo info, StreamingContext context)
        {
            User = (UserClass)info.GetValue("User", typeof(UserClass));
        }

        public void Run(IServerApp serverApp)
        {
            serverApp.CreateUser(this);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("User", User);
        }
    }
}