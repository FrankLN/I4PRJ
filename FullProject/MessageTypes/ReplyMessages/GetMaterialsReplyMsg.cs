using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ConsoleApplication1;
using DatabaseInterface;
using Server;

namespace MessageTypes.ReplyMessages
{
    public interface IGetMaterialsReplyMsg
    {
        List<MaterialClass> Materials { get; }
    }

    [Serializable()]
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

        public void Run(IClientCmd clientCmd)
        {
           clientCmd.LoadMaterials(this);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Materials", Materials);
        }
    }
}