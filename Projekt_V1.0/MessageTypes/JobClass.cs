using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseInterface
{
    [Serializable]
    public class JobClass : ISerializable
    {
        public MaterialClass Material { get; set; }
        public string Deadline { get; set; }
        public UserClass Owner { get; set; }
        public string File { get; set; }
        public string CreationTime { get; set; }
        public long FileSize { get; set; }

        public JobClass(SerializationInfo info, StreamingContext context)
        {
            Material = (MaterialClass)info.GetValue("Material", typeof(MaterialClass));
            //Hollow = (bool)info.GetValue("Hollow", typeof(bool));
            Deadline = (string)info.GetValue("Deadline", typeof(string));
            File = (string)info.GetValue("File", typeof(string));
            Owner = (UserClass)info.GetValue("Owner", typeof(UserClass));
            CreationTime = (string)info.GetValue("CreationTime", typeof(string));
            FileSize = (long)info.GetValue("FileSize", typeof(long));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Material", Material);
            //info.AddValue("Hollow", Hollow);
            info.AddValue("Deadline", Deadline);
            info.AddValue("File", File);
            info.AddValue("Owner", Owner);
            info.AddValue("CreationTime", CreationTime);
            info.AddValue("FileSize", FileSize);

        }
    }
}
