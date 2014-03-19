using System.Collections.Generic;
using System.Runtime.Serialization;
using DatabaseInterface;
using Server;

namespace MessageTypes.ReplyMessages
{
    public interface IGetMaterialsReplyMsg
    {
        List<MaterialClass> Materials { get; }
    }

    public class GetMaterialsReplyMsg : IReplyMessage, ISerializable, IGetMaterialsReplyMsg
    {
        public List<MaterialClass> Materials { get; set; }

        public GetMaterialsReplyMsg()
        {

        }

        public GetMaterialsReplyMsg(SerializationInfo info, StreamingContext context)
        {
            Materials = (List<MaterialClass>)info.GetValue("Materials", typeof(List<MaterialClass>));

        }

        public void Run(IServerApp serverApp)
        {
           
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Materials", Materials);
        }
    }
}