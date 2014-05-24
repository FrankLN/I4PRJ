using System;
using System.Runtime.Serialization;

namespace MessageTypes.Messages
{
    public interface IGetMaterialsMsg
    {
        
    }
    [Serializable()]
    public class GetMaterialsMsg : IMessage, ISerializable, IGetMaterialsMsg
    {
        public GetMaterialsMsg()
        {

        }

        public GetMaterialsMsg(SerializationInfo info, StreamingContext context)
        {
            
        }

        public void Run(IServerApp serverApp, IServer server)
        {
            serverApp.GetMaterials(this, server);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            
        }
    }
}