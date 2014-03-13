using System.Runtime.Serialization;
using Server;

namespace MessageTypes.Messages
{
    public class GetMaterialsMsg : IMessage, ISerializable
    {
        public GetMaterialsMsg()
        {

        }

        public GetMaterialsMsg(SerializationInfo info, StreamingContext context)
        {
            
        }

        public void Run(IServerApp serverApp)
        {
            serverApp.RequestJobs();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            
        }
    }
}