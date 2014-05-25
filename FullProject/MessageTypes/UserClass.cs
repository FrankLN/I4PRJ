using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseInterface
{
    /// <summary>
    /// <c>UserClass</c> is the class version of the table Customer.
    /// </summary>
    [Serializable()]
    public class UserClass : ISerializable, INotifyPropertyChanged
    {
        private string _fName;
        private string _lName;
        private string _email;
        private string _phone;

        /// <summary>
        /// The property <c>FirstName</c>
        /// </summary>
        public string FirstName
        {
            get { return _fName; }
            set
            {
                _fName = value;
                Notify("FirstName");
            }
        }

        /// <summary>
        /// The property <c>LastName</c>
        /// </summary>
        public string LastName
        {
            get { return _lName; }
            set
            {
                _lName = value;
                Notify("LastName");
            }
        }

        /// <summary>
        /// The property <c>Email</c>
        /// </summary>
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                Notify("Email");
            }
        }

        /// <summary>
        /// The property <c>PhoneNumber</c>
        /// </summary>
        public string PhoneNumber
        {
            get { return _phone; }

            set
            {
                _phone = value;
                Notify("PhoneNumber");
            }
        }

        /// <summary>
        /// The property <c>Password</c>
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The property <c>AdminRights</c>
        /// </summary>
        public int AdminRights { get; set; }

        /// <summary>
        /// The property <c>Activated</c>
        /// </summary>
        public int Activated { get; set; }

        /// <summary>
        /// The property <c>ActivationCode</c>
        /// </summary>
        public string ActivationCode { get; set; }

        /// <summary>
        /// The default <c>UserClass</c> constructor.
        /// </summary>
        public UserClass()
        {
            Email = "";
            FirstName = "";
            LastName = "";
            PhoneNumber = "";
            Password = "";
            AdminRights = 0;
            Activated = 0;
            ActivationCode = "";
        }

        /// <summary>
        /// Explicit <c>UserClass</c> constructor.
        /// </summary>
        /// <param name="info"> The User's data</param>
        /// <param name="context">The context</param>
        public UserClass(SerializationInfo info, StreamingContext context)
        {
            Email = (string)info.GetValue("Email", typeof(string));
            Password = (string)info.GetValue("Password", typeof(string));
            PhoneNumber = (string)info.GetValue("PhoneNumber", typeof(string));
            FirstName = (string)info.GetValue("FirstName", typeof(string));
            LastName = (string)info.GetValue("LastName", typeof(string));
            AdminRights = (int) info.GetValue("AdminRights", typeof (int));
            Activated = (int)info.GetValue("Activated", typeof(int));
            ActivationCode = (string) info.GetValue("ActivationCode", typeof (string));
        }

        /// <summary>
        /// <c>GetObjectData</c> gets the User's data.
        /// </summary>
        /// <param name="info">A container for the User's data</param>
        /// <param name="context">The context</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Email", Email);
            info.AddValue("Password", Password);
            info.AddValue("PhoneNumber", PhoneNumber);
            info.AddValue("FirstName", FirstName);
            info.AddValue("LastName", LastName);
            info.AddValue("AdminRights", AdminRights);
            info.AddValue("Activated", Activated);
            info.AddValue("ActivationCode", ActivationCode);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        void Notify(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        
    }
}
