using System;
using System.Runtime.Serialization;

namespace MessageTypes.ReplyMessages
{
    /// <summary>
    /// Interface for <c>ActivationCodeRequestReplyMsg</c>.
    /// </summary>
    public interface IActivationCodeRequestReplyMsg
    {
        /// <summary>
        /// The property <c>Accepted</c> for encapsulating.
        /// </summary>
        bool Accepted { get; }

        /// <summary>
        /// The property <c>ActivationCode</c> for encapsulating.
        /// </summary>
        string ActivationCode { get; }
    }

    /// <summary>
    /// The message which replies an <c>ActivationCodeRequestMsg</c>
    /// </summary>
    [Serializable()]
    public class ActivationCodeRequestReplyMsg : IReplyMessage, ISerializable, IActivationCodeRequestReplyMsg
    {
        /// <summary>
        /// The property <c>Accepted</c> declare if a new activation code has been linked to the user.
        /// </summary>
        public bool Accepted { get; set; }

        /// <summary>
        /// The property <c>ActivationCode</c> which contain the new activation code.
        /// </summary>
        public string ActivationCode { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ActivationCodeRequestReplyMsg()
        {

        }

        /// <summary>
        /// Explicit constructor for Serializing.
        /// </summary>
        /// <param name="info">For Serializing.</param>
        /// <param name="context">For Serializing.</param>
        public ActivationCodeRequestReplyMsg(SerializationInfo info, StreamingContext context)
        {
            Accepted = (bool)info.GetValue("Accepted", typeof(bool));
            ActivationCode = (string) info.GetValue("ActivationCode",typeof(string));

        }

        /// <summary>
        /// Runs the method <c>ActivationVerification</c> on the clientCmd.
        /// </summary>
        /// <param name="clientCmd">The clientCmd which has requested the server.</param>
        public void Run(IClientCmd clientCmd)
        {
            clientCmd.ActivationVerification(this);
        }

        /// <summary>
        /// Used for serializing.
        /// </summary>
        /// <param name="info">Used for serializing.</param>
        /// <param name="context">Used for serializing.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Accepted", Accepted);
            info.AddValue("ActivationCode", ActivationCode);

        }
    }
}