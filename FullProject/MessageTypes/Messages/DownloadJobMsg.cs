using System;
using System.Runtime.Serialization;
using DatabaseInterface;
using Server;

namespace MessageTypes.Messages
{
    public interface IDownloadJobMsg
    {
        JobClass Job { get; }
    }
    [Serializable()]
    public class DownloadJobMsg : IMessage, ISerializable, IDownloadJobMsg
    {
        public JobClass Job { get; set; }

        public DownloadJobMsg()
        {
            Job = new JobClass();
        }

        public DownloadJobMsg(SerializationInfo info, StreamingContext context)
        {
            Job = (JobClass)info.GetValue("Job", typeof(JobClass));
        }

        public void Run(IServerApp serverApp, IServer server)
        {
            serverApp.DownloadJob(this, server);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Job", Job);
        }
    }
}