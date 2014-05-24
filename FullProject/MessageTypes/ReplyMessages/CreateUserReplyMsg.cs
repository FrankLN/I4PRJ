using System;
using System.Runtime.Serialization;

namespace MessageTypes.ReplyMessages
{
    /// <summary>
    /// Interface for <c>CreateUserReplyMsg</c>.
    /// </summary>
    public interface ICreateUserReplyMsg
    {
        /// <summary>
        /// The property <c>Created</c> for encapsulating.
        /// </summary>
        bool Created { get; }
    }

    /// <summary>
    /// The message which replies a <c>CreateUserMsg</c>
    /// </summary>
    [Serializable()]
    public class CreateUserReplyMsg : IReplyMessage, ISerializable, ICreateUserReplyMsg
    {
        /// <summary>
        /// The property <c>Created</c> declare if a user is succesfully created.
        /// </summary>
        public bool Created { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public CreateUserReplyMsg()
        {

        }

        /// <summary>
        /// Explicit constructor for Serializing.
        /// </summary>
        /// <param name="info">For Serializing.</param>
        /// <param name="context">For Serializing.</param>
        public CreateUserReplyMsg(SerializationInfo info, StreamingContext context)
        {
            Created = (bool)info.GetValue("Created", typeof(bool));
        }

        /// <summary>
        /// Runs the method <c>CreateUserVerification</c> on the clientCmd.
        /// </summary>
        /// <param name="clientCmd">The clientCmd which has requested the server.</param>
        public void Run(IClientCmd clientCmd)
        {
            clientCmd.CreateUserVerification(this);
        }

        /// <summary>
        /// Used for serializing.
        /// </summary>
        /// <param name="info">Used for serializing.</param>
        /// <param name="context">Used for serializing.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Created", Created);
        }
    }
}