using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [Serializable()]
    class ProtoAction : ISerializable, IAction
    {
        private string _email;
        private string _password;
        public string Email { get { return _email; } set { _email = value; } }
        public string Password { get { return _password; } set { _password = value; } }

        public ProtoAction()
        {
            
        }

        public ProtoAction(SerializationInfo info, StreamingContext context)
        {
            Email = (string)info.GetValue("ProtoEmail", typeof(string));
            Password = (string) info.GetValue("ProtoPassword", typeof (string));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ProtoEmail", Email);
            info.AddValue("ProtoPassword", Password);

        }

        public void Run(IServerApp serverApp)
        {
            Console.WriteLine("Im a ProtoAction");
        }
    }
}
