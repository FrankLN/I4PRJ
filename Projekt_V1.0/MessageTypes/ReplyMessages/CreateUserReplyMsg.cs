using System.Runtime.Serialization;
using Server;

namespace MessageTypes.ReplyMessages
{
    public interface ICreateUserReplyMsg
    {
        bool Created { get; }
        string ActivationCode { get; }

    }

    public class CreateUserReplyMsg : IReplyMessage, ISerializable
    {
        public bool Created { get; set; }

        public string ActivationCode { get; set; }


        public CreateUserReplyMsg()
        {

        }

        public CreateUserReplyMsg(SerializationInfo info, StreamingContext context)
        {
            Created = (bool)info.GetValue("Created", typeof(bool));
            ActivationCode = (string) info.GetValue("ActivationCode", typeof (bool));
        }

        public void Run(IServerApp serverApp)
        {
            
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Created", Created);
            info.AddValue("ActivationCode", ActivationCode);

        }
    }
}