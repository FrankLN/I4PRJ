using System;
using System.Runtime.Serialization;

namespace MessageTypes.ReplyMessages
{
    /// <summary>
    /// Interface for <c>ActivationReplyMsg</c>.
    /// </summary>
    public interface IActivationReplyMsg
    {
        /// <summary>
        /// The property <c>UserActivated</c> for encapsulating.
        /// </summary>
        bool UserActivated { get; }
    }

    /// <summary>
    /// The message which replies an <c>ActivationMsg</c>
    /// </summary>
    [Serializable]
    public class ActivationReplyMsg: ISerializable, IReplyMessage,IActivationReplyMsg
    {
        /// <summary>
        /// The property <c>UserActivated</c> declare if the user has entered the right activation code
        /// and thereby become activated.
        /// </summary>
        public bool UserActivated { set; get; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ActivationReplyMsg()
        {
            UserActivated = false;
        }

        /// <summary>
        /// Explicit constructor for Serializing.
        /// </summary>
        /// <param name="info">For Serializing.</param>
        /// <param name="context">For Serializing.</param>
        public ActivationReplyMsg(SerializationInfo info, StreamingContext context)
        {
            UserActivated = (bool)info.GetValue("UserActivated", typeof(bool));
        }

        /// <summary>
        /// Used for serializing.
        /// </summary>
        /// <param name="info">Used for serializing.</param>
        /// <param name="context">Used for serializing.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("UserActivated", UserActivated);
        }

        /// <summary>
        /// Runs the method <c>ValidateActivation</c> on the clientCmd.
        /// </summary>
        /// <param name="clientCmd">The clientCmd which has requested the server.</param>
        public void Run(IClientCmd clientCmd)
        {
            clientCmd.ValidateActivation(this);
        }
    }
}
