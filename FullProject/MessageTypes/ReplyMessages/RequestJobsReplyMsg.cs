using System;
using System.Runtime.Serialization;
using DatabaseInterface;
using System.Collections.Generic;

namespace MessageTypes.ReplyMessages
{
    /// <summary>
    /// Interface for <c>RequestJobsReplyMsg</c>.
    /// </summary>
    public interface IRequestJobsReplyMsg
    {
        /// <summary>
        /// Property <c>JobList</c> for encapsulting.
        /// </summary>
        List<JobClass> JobList { get; }  
    }

    /// <summary>
    /// The message which replies a <c>RequestJobsMsg</c>
    /// </summary>
    [Serializable()]
    public class RequestJobsReplyMsg : IReplyMessage, ISerializable, IRequestJobsReplyMsg
    {
        /// <summary>
        /// The property <c>JobList</c> contains the jobs which are send back to the client.
        /// </summary>
        public List<JobClass> JobList { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public RequestJobsReplyMsg()
        {
            
        }

        /// <summary>
        /// Explicit constructor for Serializing.
        /// </summary>
        /// <param name="info">For Serializing.</param>
        /// <param name="context">For Serializing.</param>
        public RequestJobsReplyMsg(SerializationInfo info, StreamingContext context)
        {
            JobList = (List<JobClass>)info.GetValue("jobList", typeof(List<JobClass>));

        }

        /// <summary>
        /// Used for serializing.
        /// </summary>
        /// <param name="info">Used for serializing.</param>
        /// <param name="context">Used for serializing.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("jobList", JobList);

        }

        /// <summary>
        /// Runs the method <c>LoadJobList</c> on the clientCmd.
        /// </summary>
        /// <param name="clientCmd">The clientCmd which has requested the server.</param>
        public void Run(IClientCmd clientCmd)
        {
            clientCmd.LoadJobList(this);
        }
    }
}