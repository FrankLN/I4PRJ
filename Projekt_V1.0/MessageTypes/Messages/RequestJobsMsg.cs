using System.Runtime.Serialization;
using Server;

namespace MessageTypes.Messages
{
    public class RequestJobsMsg : IMessage, ISerializable
    {
        public RequestJobsMsg()
        {

        }

        public RequestJobsMsg(SerializationInfo info, StreamingContext context)
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