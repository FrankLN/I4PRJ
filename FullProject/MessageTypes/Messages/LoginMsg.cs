using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace MessageTypes.Messages
{
    /// <summary>
    /// Interface for <c>LoginMsg</c>.
    /// </summary>
    public interface ILoginMsg
    {
        /// <summary>
        /// Property <c>Email</c> used for encapsulating.
        /// </summary>
        string Email { get; }
        /// <summary>
        /// Property <c>Password</c> used for encapsulating.
        /// </summary>
        string Password { get; }
    }

    /// <summary>
    /// <c>LoginMsg</c> is the message class which let a user login
    /// if the login informations are correct.
    /// </summary>
    [Serializable]
    public class LoginMsg : IMessage, ISerializable, ILoginMsg, INotifyPropertyChanged 
    {
        private string email;

        /// <summary>
        /// The property <c>Email</c> to identify the user which requested to login.
        /// Notifies changes for validating using the <c>Notify</c> Method.
        /// </summary>
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                Notify("Email");
            }
        }

        /// <summary>
        /// The property <c>Password</c> to secure the user.
        /// Notifies changes for validating using the <c>Notify</c> Method.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public LoginMsg()
        {
            
        }

        /// <summary>
        /// Explicit constructor used for Serializing.
        /// </summary>
        /// <param name="info">Used for Serializing.</param>
        /// <param name="context">Used for Serializing.</param>
        public LoginMsg(SerializationInfo info, StreamingContext context)
        {
            Email = (string)info.GetValue("email", typeof(string));
            Password = (string) info.GetValue("password", typeof (string));
        }

        /// <summary>
        /// The <c>Run</c> Method is called when recieved by the <c>Server</c>.
        /// </summary>
        /// <param name="serverApp">The <c>ServerApp</c> used to call the 
        /// <c>ActivationCodeRequest</c> method.</param>
        /// <param name="server">The <c>Server</c> instance used for replying.</param>
        public void Run(IServerApp serverApp, IServer server)
        {
            serverApp.VerifyLogin(this, server);
        }

        /// <summary>
        /// <c>GetOcjectData</c> is a method used for Serializing.
        /// </summary>
        /// <param name="info">User for Serializing.</param>
        /// <param name="context">Used for Serializing.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("email", Email);
            info.AddValue("password", Password);
        }

        /// <summary>
        /// Raise an event when a property is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Method used for validating.
        /// </summary>
        /// <param name="prop">The property which has changed and need new validating</param>
        void Notify(string prop) { if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(prop)); } }
    }
}