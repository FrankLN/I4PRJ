using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseInterface
{
    [Serializable()]
    public class UserClass : ISerializable, INotifyPropertyChanged
    {
        private string _fName;
        private string _lName;
        private string _email;
        private string _phone;

        public string FirstName
        {
            get { return _fName; }
            set
            {
                _fName = value;
                Notify("FirstName");
            }
        }

        public string LastName
        {
            get { return _lName; }
            set
            {
                _lName = value;
                Notify("LastName");
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                Notify("Email");
            }
        }

        public string PhoneNumber
        {
            get { return _phone; }

            set
            {
                _phone = value;
                Notify("Phonenumber");
            }
        }
        public string Password { get; set; }
        public int AdminRights { get; set; }
        public int Activated { get; set; }
        public string ActivationCode { get; set; }

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
