using System.Runtime.Serialization;
using DatabaseInterface;
using Server;
using System.Collections.Generic;

namespace MessageTypes.ReplyMessages
{
    public interface IRequestJobsReplyMsg
    {
        List<JobClass> JobList { get; }  
    }

    public class RequestJobsReplyMsg : IReplyMessage, ISerializable, IRequestJobsReplyMsg
    {
        public List<JobClass> JobList { get; set; }

        public RequestJobsReplyMsg()
        {
            
        }

        public RequestJobsReplyMsg(SerializationInfo info, StreamingContext context)
        {
            JobList = (List<JobClass>)info.GetValue("jobList", typeof(List<JobClass>));

        }

        public void Run(IServerApp serverApp)
        {
            
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("jobList", JobList);

        }

    }
}