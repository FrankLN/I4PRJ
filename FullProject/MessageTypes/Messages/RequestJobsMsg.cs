using System;
using System.Runtime.Serialization;

namespace MessageTypes.Messages
{

    public interface IRequestJobsMsg
    {
        
    }
    [Serializable()]
    public class RequestJobsMsg : IMessage, ISerializable, IRequestJobsMsg
    {
        public RequestJobsMsg()
        {

        }

        public RequestJobsMsg(SerializationInfo info, StreamingContext context)
        {
            
        }

        public void Run(IServerApp serverApp, IServer server)
        {
            serverApp.RequestJobs(this, server);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            
        }
    }
}