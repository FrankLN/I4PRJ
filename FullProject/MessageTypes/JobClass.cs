using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseInterface
{
    [Serializable()]
    public class JobClass : ISerializable
    {
        public int OrderId { get; set; }
        public MaterialClass Material { get; set; }
        public string Deadline { get; set; }
        public UserClass Owner { get; set; }
        public string File { get; set; }
        public long FileSize { get; set; }
        public string CreationTime { get; set; }
        public int Hollow { get; set; }
        public string Comment { get; set; }
        public int Status { get; set; }

        public JobClass()
        {
            OrderId = 0;
            Material = new MaterialClass();
            Deadline = "";
            Owner = new UserClass();
            File = "";
            CreationTime = "";
            Hollow = 0;             // Hollow is set to 0 as the 3DJob is full by default.
            Comment = "";
            Status = 0;
        }

        public JobClass(SerializationInfo info, StreamingContext context)
        {
            Material = (MaterialClass)info.GetValue("Material", typeof(MaterialClass));
            OrderId = (int) info.GetValue("OrderId", typeof (int));
            Hollow = (int)info.GetValue("Hollow", typeof(int));
            FileSize = (long)info.GetValue("FileSize", typeof(long));
            Deadline = (string)info.GetValue("Deadline", typeof(string));
            File = (string)info.GetValue("File", typeof(string));
            Owner = (UserClass)info.GetValue("Owner", typeof(UserClass));
            CreationTime = (string)info.GetValue("CreationTime", typeof(string));
            Comment = (string)info.GetValue("Comment", typeof(string));
            Status = (int) info.GetValue("Status", typeof (int));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Material", Material);
            info.AddValue("OrderId", OrderId);
            info.AddValue("Hollow", Hollow);
            info.AddValue("Deadline", Deadline);
            info.AddValue("File", File);
            info.AddValue("FileSize", FileSize);
            info.AddValue("Owner", Owner);
            info.AddValue("CreationTime", CreationTime);
            info.AddValue("Comment", Comment);
            info.AddValue("Status", Status);
        }
    }
}
