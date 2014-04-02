using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1;

namespace MessageTypes.ReplyMessages
{
    public interface IActivationReplyMsg
    {
        bool UserActivated { get; }
    }

    public class ActivationReplyMsg: ISerializable, IReplyMessage,IActivationReplyMsg
    {
        public bool UserAvctivated { set; get; }
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
            throw new NotImplementedException();
        }

        public bool UserActivated { get; private set; }
    }
}
