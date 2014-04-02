using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DatabaseInterface;
using Server;

namespace MessageTypes.Messages
{
    public interface IActivationMsg
    {
        UserClass User { get; }
    }
    class ActivationMsg : ISerializable, IMessage, IActivationMsg
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

        public void Run(IServerApp serverApp)
        {
            serverApp.ActivateUser(this);
        }

        public void Run(IServerApp serverApp, IServer server)
        {
            serverApp.ActivateUser(this);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("User", User);
        }
    }
}
