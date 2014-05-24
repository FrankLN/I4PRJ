using System;
using System.Runtime.Serialization;

namespace MessageTypes.ReplyMessages
{
    public interface IActivationCodeRequestReplyMsg
    {
        bool Accepted { get; }
        string ActivationCode { get; }
    }

    [Serializable()]
    public class ActivationCodeRequestReplyMsg : IReplyMessage, ISerializable, IActivationCodeRequestReplyMsg
    {
        public bool Accepted { get; set; }
        public string ActivationCode { get; set; }

        public ActivationCodeRequestReplyMsg()
        {

        }

        public ActivationCodeRequestReplyMsg(SerializationInfo info, StreamingContext context)
        {
            Accepted = (bool)info.GetValue("Accepted", typeof(bool));
            ActivationCode = (string) info.GetValue("ActivationCode",typeof(string));

        }

        public void Run(IClientCmd clientCmd)
        {

            clientCmd.ActivationVerification(this);
            
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Accepted", Accepted);
            info.AddValue("ActivationCode", ActivationCode);

        }
    }
}