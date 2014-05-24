using System;
using System.Runtime.Serialization;
using DatabaseInterface;

namespace MessageTypes.Messages
{
    /// <summary>
    /// Interface for <c>DownloadJobMsg</c>.
    /// </summary>
    public interface IDownloadJobMsg
    {
        /// <summary>
        /// Property <c>Job</c> used for encapsulating.
        /// </summary>
        JobClass Job { get; }
    }

    /// <summary>
    /// <c>DownloadJobMsg</c> is the message class which request the server for a 
    /// Job to download.
    /// </summary>
    [Serializable()]
    public class DownloadJobMsg : IMessage, ISerializable, IDownloadJobMsg
    {
        /// <summary>
        /// The property <c>Job</c> to identify the job which is requested from the <c>Server</c>.
        /// </summary>
        public JobClass Job { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DownloadJobMsg()
        {
            Job = new JobClass();
        }

        /// <summary>
        /// Explicit constructor used for Serializing.
        /// </summary>
        /// <param name="info">Used for Serializing.</param>
        /// <param name="context">Used for Serializing.</param>
        public DownloadJobMsg(SerializationInfo info, StreamingContext context)
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
            serverApp.DownloadJob(this, server);
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