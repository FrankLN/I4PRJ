using System;
using System.Runtime.Serialization;

namespace MessageTypes.Messages
{
    /// <summary>
    /// Interface for <c>ActivationCodeRequestMsg</c>.
    /// </summary>
    public interface IActivationCodeRequestMsg
    {
        /// <summary>
        /// Property <c>Email</c> used for encapsulating.
        /// </summary>
        string Email { get; }
    }

    /// <summary>
    /// <c>ActivationCodeRequestMsg</c> is the message class which request the server for a 
    /// new activation code.
    /// </summary>
    [Serializable()]
    public class ActivationCodeRequestMsg : IMessage, ISerializable, IActivationCodeRequestMsg
    {
        /// <summary>
        /// The property <c>Email</c> to identify the user which requested a new activation code.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ActivationCodeRequestMsg()
        {
        }

        /// <summary>
        /// Explicit constructor used for Serializing.
        /// </summary>
        /// <param name="info">Used for Serializing.</param>
        /// <param name="context">Used for Serializing.</param>
        public ActivationCodeRequestMsg(SerializationInfo info, StreamingContext context)
        {
            Email = (string) info.GetValue("Email", typeof (string));
        }

        /// <summary>
        /// The <c>Run</c> Method is called when recieved by the <c>Server</c>.
        /// </summary>
        /// <param name="serverApp">The <c>ServerApp</c> used to call the 
        /// <c>ActivationCodeRequest</c> method.</param>
        /// <param name="server">The <c>Server</c> instance used for replying.</param>
        public void Run(IServerApp serverApp, IServer server)
        {
            serverApp.ActivationCodeRequest(this, server);
        }

        /// <summary>
        /// <c>GetOcjectData</c> is a method used for Serializing.
        /// </summary>
        /// <param name="info">User for Serializing.</param>
        /// <param name="context">Used for Serializing.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Email", Email);
        }
    }
}