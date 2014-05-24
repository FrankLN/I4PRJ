using System;
using System.Runtime.Serialization;
using DatabaseInterface;

namespace MessageTypes.Messages
{
    public interface ICreateJobMsg
    {
        JobClass Job { get; }
    }
    [Serializable()]
    public class CreateJobMsg : IMessage, ISerializable, ICreateJobMsg
    {
        public JobClass Job { get; set; }

        public CreateJobMsg()
        {

        }

        public CreateJobMsg(SerializationInfo info, StreamingContext context)
        {
            Job = (JobClass)info.GetValue("Job", typeof(JobClass));
       
        }

        public void Run(IServerApp serverApp, IServer server)
        {
            serverApp.CreateJob(this, server);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Job", Job);
        }
    }
}