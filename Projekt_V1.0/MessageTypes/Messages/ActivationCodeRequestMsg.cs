using System.Runtime.Serialization;
using Server;

namespace MessageTypes.Messages
{
    public interface IActivationCodeRequestMsg
    {
        string Email { get; }
    }
    public class ActivationCodeRequestMsg : IMessage, ISerializable, IActivationCodeRequestMsg
    {
        public string Email { get; set; }

        public ActivationCodeRequestMsg()
        {

        }

        public ActivationCodeRequestMsg(SerializationInfo info, StreamingContext context)
        {
            Email = (string) info.GetValue("Email", typeof (string));
        }

        public void Run(IServerApp serverApp)
        {
            serverApp.ActivationCodeRequest(this);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Email", Email);
        }
    }
}