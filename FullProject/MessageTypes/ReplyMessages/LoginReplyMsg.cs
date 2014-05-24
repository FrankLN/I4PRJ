using System;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using DatabaseInterface;

namespace MessageTypes.ReplyMessages
{
    /// <summary>
    /// Interface for <c>LoginReplyMsg</c>.
    /// </summary>
    public interface ILoginReplyMsg
    {
        /// <summary>
        /// The property <c>Email</c> for encapsulating.
        /// </summary>
        bool Email { get; }

        /// <summary>
        /// The property <c>Password</c> for encapsulating.
        /// </summary>
        bool Password { get; }

        /// <summary>
        /// The property <c>Activated</c> for encapsulating.
        /// </summary>
        bool Activated { get; }

        /// <summary>
        /// The property <c>User</c> for encapsulating.
        /// </summary>
        UserClass User { get; }
    }

    /// <summary>
    /// The message which replies a <c>LoginMsg</c>
    /// </summary>
    [Serializable()]
    public class LoginReplyMsg : IReplyMessage, ISerializable,ILoginReplyMsg
    {
        /// <summary>
        /// The property <c>Email</c> declare if the email exist.
        /// </summary>
        public bool Email { get; set; }

        /// <summary>
        /// The property <c>Password</c> declare if the password is correct for the email.
        /// </summary>
        public bool Password { get; set; }

        /// <summary>
        /// The property <c>User</c> contains the userinfo of the user who is trying to login.
        /// </summary>
        public UserClass User { get; set; }

        /// <summary>
        /// The property <c>Activated</c> declare if the user is activated.
        /// </summary>
        public bool Activated { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public LoginReplyMsg()
        {
            
        }

        /// <summary>
        /// Explicit constructor for Serializing.
        /// </summary>
        /// <param name="info">For Serializing.</param>
        /// <param name="context">For Serializing.</param>
        public LoginReplyMsg(SerializationInfo info, StreamingContext context)
        {
            Email = (bool)info.GetValue("email", typeof(bool));
            Password = (bool) info.GetValue("password", typeof (bool));
            Activated = (bool) info.GetValue("Activated", typeof (bool));
            User = (UserClass)info.GetValue("User", typeof(UserClass));
        }

        /// <summary>
        /// Used for serializing.
        /// </summary>
        /// <param name="info">Used for serializing.</param>
        /// <param name="context">Used for serializing.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("email", Email);
            info.AddValue("password", Password);
            info.AddValue("Activated", Activated);
            info.AddValue("User", User);
        }

        /// <summary>
        /// Runs the method <c>LoginVerification</c> on the clientCmd.
        /// </summary>
        /// <param name="clientCmd">The clientCmd which has requested the server.</param>
        public void Run(IClientCmd clientCmd)
        {
            clientCmd.LoginVerification(this);
        }
    }
}