using System;
using System.Runtime.Serialization;

namespace MessageTypes.Messages
{
    /// <summary>
    /// Interface for <c>RequestJobsMsg</c>.
    /// </summary>
    public interface IRequestJobsMsg
    {   
    }

    /// <summary>
    /// <c>RequestJobsMsg</c> is the message class which request the server for all 
    /// Jobs in the database.
    /// </summary>
    [Serializable()]
    public class RequestJobsMsg : IMessage, ISerializable, IRequestJobsMsg
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public RequestJobsMsg()
        {

        }

        /// <summary>
        /// Explicit constructor used for Serializing.
        /// </summary>
        /// <param name="info">Used for Serializing.</param>
        /// <param name="context">Used for Serializing.</param>
        public RequestJobsMsg(SerializationInfo info, StreamingContext context)
        {
            
        }

        /// <summary>
        /// The <c>Run</c> Method is called when recieved by the <c>Server</c>.
        /// </summary>
        /// <param name="serverApp">The <c>ServerApp</c> used to call the 
        /// <c>ActivationCodeRequest</c> method.</param>
        /// <param name="server">The <c>Server</c> instance used for replying.</param>
        public void Run(IServerApp serverApp, IServer server)
        {
            serverApp.RequestJobs(this, server);
        }

        /// <summary>
        /// <c>GetOcjectData</c> is a method used for Serializing.
        /// </summary>
        /// <param name="info">User for Serializing.</param>
        /// <param name="context">Used for Serializing.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            
        }
    }
}