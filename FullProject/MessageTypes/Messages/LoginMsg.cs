using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace MessageTypes.Messages
{
    public interface ILoginMsg
    {
        string Email { get; }
        string Password { get; }
    }

    [Serializable]
    public class LoginMsg : IMessage, ISerializable, ILoginMsg, INotifyPropertyChanged 
    {
        private string email;

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                Notify("Email");
            }
        }
        public string Password { get; set; }

        public LoginMsg()
        {
            
        }

        public LoginMsg(SerializationInfo info, StreamingContext context)
        {
            Email = (string)info.GetValue("email", typeof(string));
            Password = (string) info.GetValue("password", typeof (string));
        }

        public void Run(IServerApp serverApp, IServer server)
        {
            serverApp.VerifyLogin(this, server);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("email", Email);
            info.AddValue("password", Password);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        void Notify(string prop) { if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(prop)); } }
    }
}