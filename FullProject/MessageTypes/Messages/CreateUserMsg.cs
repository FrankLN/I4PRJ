using System;
using System.Runtime.Serialization;
using DatabaseInterface;
using Server;

namespace MessageTypes.Messages
{
    public interface ICreateUserMsg
    {
        UserClass User { get; }
    }

    [Serializable()]
    public class CreateUserMsg : IMessage, ISerializable, ICreateUserMsg
    {
        public UserClass User { get; set; }

        public CreateUserMsg()
        {
            User = new UserClass();
        }

        public CreateUserMsg(SerializationInfo info, StreamingContext context)
        {
            User = (UserClass)info.GetValue("User", typeof(UserClass));
        }

        public void Run(IServerApp serverApp, IServer server)
        {
            serverApp.CreateUser(this, server);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("User", User);
        }
    }
}