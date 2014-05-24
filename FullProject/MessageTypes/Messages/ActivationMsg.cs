using System;
using System.Runtime.Serialization;
using DatabaseInterface;

namespace MessageTypes.Messages
{
    public interface IActivationMsg
    {
        UserClass User { get; }
    }
    [Serializable]
    public class ActivationMsg : ISerializable, IMessage, IActivationMsg
    {
        public UserClass User { get; set; }

        public ActivationMsg()
        {
            User = new UserClass();
        }
        public ActivationMsg(SerializationInfo info, StreamingContext context)
        {
            User = (UserClass)info.GetValue("User", typeof(UserClass));
        }

        public void Run(IServerApp serverApp, IServer server)
        {
            serverApp.ActivateUser(this, server);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("User", User);
        }
    }
}
