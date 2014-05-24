using System;
using System.Runtime.Serialization;
using DatabaseInterface;

namespace MessageTypes.ReplyMessages
{
    public interface IDownloadJobReplyMsg
    {
        JobClass Job { get; }
    }

    [Serializable()]
    public class DownloadJobReplyMsg : IReplyMessage, ISerializable, IDownloadJobReplyMsg
    {
        public JobClass Job { get; set; }

        public DownloadJobReplyMsg()
        {
            Job = new JobClass();
        }

        public DownloadJobReplyMsg(SerializationInfo info, StreamingContext context)
        {
            Job = (JobClass)info.GetValue("Job", typeof(JobClass));
        }

        public void Run(IClientCmd clientCmd)
        {
            clientCmd.DownloadCommencing(this);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Job", Job);
        }
    }
}