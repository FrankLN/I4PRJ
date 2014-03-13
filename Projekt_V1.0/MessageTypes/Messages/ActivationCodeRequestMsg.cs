using System.Runtime.Serialization;
using Server;

namespace MessageTypes.Messages
{
    public class ActivationCodeRequestMsg : IMessage, ISerializable
    {
        public string FileName { get; set; }

        public ActivationCodeRequestMsg()
        {

        }

        public ActivationCodeRequestMsg(SerializationInfo info, StreamingContext context)
        {
            FileName = (string) info.GetValue("FileName", typeof (string));
        }

        public void Run(IServerApp serverApp)
        {
            serverApp.ActivationCodeRequest(FileName);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("FileName", FileName);
        }
    }
}