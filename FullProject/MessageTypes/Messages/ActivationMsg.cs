using System;
using System.Runtime.Serialization;
using DatabaseInterface;

namespace MessageTypes.Messages
{
    /// <summary>
    /// Interface for <c>ActivationMsg</c>.
    /// </summary>
    public interface IActivationMsg
    {
        /// <summary>
        /// The property <c>User</c> used for encapsulting.
        /// </summary>
        UserClass User { get; }
    }

    /// <summary>
    /// <c>ActivationMsg</c> is the message class which request activation for a user.
    /// </summary>
    [Serializable]
    public class ActivationMsg : ISerializable, IMessage, IActivationMsg
    {
        /// <summary>
        /// The property <c>User</c> to identify the user which requested to be activated.
        /// </summary>
        public UserClass User { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ActivationMsg()
        {
            User = new UserClass();
        }

        /// <summary>
        /// Explicit constructor used for Serializing.
        /// </summary>
        /// <param name="info">Used for Serializing.</param>
        /// <param name="context">Used for Serializing.</param>
        public ActivationMsg(SerializationInfo info, StreamingContext context)
        {
            User = (UserClass)info.GetValue("User", typeof(UserClass));
        }

        /// <summary>
        /// The <c>Run</c> Method is called when recieved by the <c>Server</c>.
        /// </summary>
        /// <param name="serverApp">The <c>ServerApp</c> used to call the 
        /// <c>ActivationCodeRequest</c> method.</param>
        /// <param name="server">The <c>Server</c> instance used for replying.</param>
        public void Run(IServerApp serverApp, IServer server)
        {
            serverApp.ActivateUser(this, server);
        }

        /// <summary>
        /// <c>GetOcjectData</c> is a method used for Serializing.
        /// </summary>
        /// <param name="info">User for Serializing.</param>
        /// <param name="context">Used for Serializing.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("User", User);
        }
    }
}
