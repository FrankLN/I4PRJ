using System;
using System.Runtime.Serialization;

namespace MessageTypes.ReplyMessages
{
    public interface IActivationReplyMsg
    {
        bool UserActivated { get; }
    }
    [Serializable]
    public class ActivationReplyMsg: ISerializable, IReplyMessage,IActivationReplyMsg
    {
        public bool UserActivated { set; get; }
        public ActivationReplyMsg()
        {
            UserActivated = false;
        }

        public ActivationReplyMsg(SerializationInfo info, StreamingContext context)
        {
            UserActivated = (bool)info.GetValue("UserActivated", typeof(bool));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("UserActivated", UserActivated);
        }

        public void Run(IClientCmd clientCmd)
        {
            clientCmd.ValidateActivation(this);
        }
    }
}
