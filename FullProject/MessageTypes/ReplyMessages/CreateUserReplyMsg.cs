using System;
using System.Runtime.Serialization;
using ConsoleApplication1;
using Server;

namespace MessageTypes.ReplyMessages
{
    public interface ICreateUserReplyMsg
    {
        bool Created { get; }
    }

    [Serializable()]
    public class CreateUserReplyMsg : IReplyMessage, ISerializable, ICreateUserReplyMsg
    {
        public bool Created { get; set; }


        public CreateUserReplyMsg()
        {

        }

        public CreateUserReplyMsg(SerializationInfo info, StreamingContext context)
        {
            Created = (bool)info.GetValue("Created", typeof(bool));
        }

        public void Run(IClientCmd clientCmd)
        {
            clientCmd.CreateUserVerification(this);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Created", Created);
        }
    }
}