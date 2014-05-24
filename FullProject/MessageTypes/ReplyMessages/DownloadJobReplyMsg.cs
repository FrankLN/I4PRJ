using System;
using System.Runtime.Serialization;
using DatabaseInterface;

namespace MessageTypes.ReplyMessages
{
    /// <summary>
    /// Interface for <c>DownloadJobReplyMsg</c>.
    /// </summary>
    public interface IDownloadJobReplyMsg
    {
        /// <summary>
        /// The property <c>Job</c> for encapsulating.
        /// </summary>
        JobClass Job { get; }
    }

    /// <summary>
    /// The message which replies a <c>DownloadJobMsg</c>
    /// </summary>
    [Serializable()]
    public class DownloadJobReplyMsg : IReplyMessage, ISerializable, IDownloadJobReplyMsg
    {
        /// <summary>
        /// The property <c>Job</c> is the job requested from the client.
        /// </summary>
        public JobClass Job { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DownloadJobReplyMsg()
        {
            Job = new JobClass();
        }

        /// <summary>
        /// Explicit constructor for Serializing.
        /// </summary>
        /// <param name="info">For Serializing.</param>
        /// <param name="context">For Serializing.</param>
        public DownloadJobReplyMsg(SerializationInfo info, StreamingContext context)
        {
            Job = (JobClass)info.GetValue("Job", typeof(JobClass));
        }

        /// <summary>
        /// Runs the method <c>DownloadCommencing</c> on the clientCmd.
        /// </summary>
        /// <param name="clientCmd">The clientCmd which has requested the server.</param>
        public void Run(IClientCmd clientCmd)
        {
            clientCmd.DownloadCommencing(this);
        }

        /// <summary>
        /// Used for serializing.
        /// </summary>
        /// <param name="info">Used for serializing.</param>
        /// <param name="context">Used for serializing.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Job", Job);
        }
    }
}