using System;
using System.Runtime.Serialization;
using DatabaseInterface;

namespace MessageTypes.Messages
{
    /// <summary>
    /// Interface for <c>CreateJobMsg</c>.
    /// </summary>
    public interface ICreateJobMsg
    {
        /// <summary>
        /// Property <c>Job</c> used for encapsulating.
        /// </summary>
        JobClass Job { get; }
    }

    /// <summary>
    /// <c>CreateJobMsg</c> is the message class which uploads a job to the server
    /// </summary>
    [Serializable()]
    public class CreateJobMsg : IMessage, ISerializable, ICreateJobMsg
    {
        /// <summary>
        /// The property <c>Job</c> to be created.
        /// </summary>
        public JobClass Job { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public CreateJobMsg()
        {

        }

        /// <summary>
        /// Explicit constructor used for Serializing.
        /// </summary>
        /// <param name="info">Used for Serializing.</param>
        /// <param name="context">Used for Serializing.</param>
        public CreateJobMsg(SerializationInfo info, StreamingContext context)
        {
            Job = (JobClass)info.GetValue("Job", typeof(JobClass));
       
        }

        /// <summary>
        /// The <c>Run</c> Method is called when recieved by the <c>Server</c>.
        /// </summary>
        /// <param name="serverApp">The <c>ServerApp</c> used to call the 
        /// <c>ActivationCodeRequest</c> method.</param>
        /// <param name="server">The <c>Server</c> instance used for replying.</param>
        public void Run(IServerApp serverApp, IServer server)
        {
            serverApp.CreateJob(this, server);
        }

        /// <summary>
        /// <c>GetOcjectData</c> is a method used for Serializing.
        /// </summary>
        /// <param name="info">User for Serializing.</param>
        /// <param name="context">Used for Serializing.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Job", Job);
        }
    }
}