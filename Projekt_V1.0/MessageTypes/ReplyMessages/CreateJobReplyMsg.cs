using System.Runtime.Serialization;
using Server;

namespace MessageTypes.ReplyMessages
{
    public interface ICreateJobReplyMsg
    {
        bool Created { get; }
    }
    public class CreateJobReplyMsg : IReplyMessage, ISerializable, ICreateJobReplyMsg
    {
        public bool Created { get; set; }


        public CreateJobReplyMsg()
        {

        }

        public CreateJobReplyMsg(SerializationInfo info, StreamingContext context)
        {
            Created = (bool)info.GetValue("Created", typeof(bool));

        }

        public void Run(IServerApp serverApp)
        {
            
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Created", Created);

        }
    }
}