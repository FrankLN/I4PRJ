using System;
using System.Runtime.Serialization;
using MessageTypes.ReplyMessages;
using Server;

namespace MessageTypes.Messages
{
    public interface ILoginMsg
    {
        string Email { get; }
        string Password { get; }
    }

    [Serializable]
    public class LoginMsg : IMessage, ISerializable, ILoginMsg
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public LoginMsg()
        {
            
        }

        public LoginMsg(SerializationInfo info, StreamingContext context)
        {
            Email = (string)info.GetValue("email", typeof(string));
            Password = (string) info.GetValue("password", typeof (string));
        }

        public void Run(IServerApp serverApp)
        {
            serverApp.VerifyLogin(this);
        }

        public void Run(IServerApp serverApp, IServer server)
        {
            serverApp.VerifyLogin(this, server);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("email", Email);
            info.AddValue("password", Password);
        }
    }
}