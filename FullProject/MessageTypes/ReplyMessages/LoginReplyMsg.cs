using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using ConsoleApplication1;
using DatabaseInterface;
using Server;

namespace MessageTypes.ReplyMessages
{
    public interface ILoginReplyReplyMsg
    {
        bool Email { get; }
        bool Password { get; }
        UserClass User { get; }

    }

    public class LoginReplyMsg : IReplyMessage, ISerializable,ILoginReplyReplyMsg
    {
        public bool Email { get; set; }
        public bool Password { get; set; }
        public UserClass User { get; set; }

        public LoginReplyMsg()
        {
            
        }

        public LoginReplyMsg(SerializationInfo info, StreamingContext context)
        {
            Email = (bool)info.GetValue("email", typeof(bool));
            Password = (bool) info.GetValue("password", typeof (bool));
            User = (UserClass)info.GetValue("User", typeof(UserClass));
            
        }


        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("email", Email);
            info.AddValue("password", Password);
            info.AddValue("User", User);

        }

        public void Run(IClientCmd clientCmd)
        {
            clientCmd.LoginVerification(this);
        }
    }
}