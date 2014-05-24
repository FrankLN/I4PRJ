using System;
using System.Runtime.Serialization;
using DatabaseInterface;

namespace MessageTypes.Messages
{
    /// <summary>
    /// Interface for <c>CreateUserMsg</c>.
    /// </summary>
    public interface ICreateUserMsg
    {
        /// <summary>
        /// Property <c>User</c> used for encapsulating.
        /// </summary>
        UserClass User { get; }
    }

    /// <summary>
    /// <c>CreateUserMsg</c> is the message class which creates a new user on the <c>Server</c>.
    /// </summary>
    [Serializable()]
    public class CreateUserMsg : IMessage, ISerializable, ICreateUserMsg
    {
        /// <summary>
        /// The property <c>User</c> to be created.
        /// </summary>
        public UserClass User { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public CreateUserMsg()
        {
            User = new UserClass();
        }

        /// <summary>
        /// Explicit constructor used for Serializing.
        /// </summary>
        /// <param name="info">Used for Serializing.</param>
        /// <param name="context">Used for Serializing.</param>
        public CreateUserMsg(SerializationInfo info, StreamingContext context)
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
            serverApp.CreateUser(this, server);
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