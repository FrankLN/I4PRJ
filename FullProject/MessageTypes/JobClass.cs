using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using MessageTypes.Messages;

namespace DatabaseInterface
{
    /// <summary>
    /// <c>JobClass</c> is the class version of the table My3DJob.
    /// </summary>
    [Serializable()]
    public class JobClass : ISerializable
    {
        /// <summary>
        /// The property <c>OrderId</c>
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// The property <c>Material</c>
        /// </summary>
        public MaterialClass Material { get; set; }

        /// <summary>
        /// The property <c>Deadline</c>
        /// </summary>
        public string Deadline { get; set; }

        /// <summary>
        /// The property <c>Owner</c>
        /// </summary>
        public UserClass Owner { get; set; }

        /// <summary>
        /// The property <c>File</c>
        /// </summary>
        public string File { get; set; }

        /// <summary>
        /// The property <c>FileSize</c>
        /// </summary>
        public long FileSize { get; set; }

        /// <summary>
        /// The proporty <c>CreationTime</c>
        /// </summary>
        public string CreationTime { get; set; }

        /// <summary>
        /// The property <c>Hollow</c>
        /// </summary>
        public int Hollow { get; set; }

        /// <summary>
        /// The property <c>Comment</c>
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// The property <c>Status</c>
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// The default <c>JobClass</c> constructor.
        /// </summary>
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

        /// <summary>
        /// The explicit <c>JobClass</c> constructor.
        /// </summary>
        /// <param name="info">The job's data</param>
        /// <param name="context">The context</param>
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

        /// <summary>
        /// <c>GetObjectData</c> gets the job's data.
        /// </summary>
        /// <param name="info">A container for the job's data</param>
        /// <param name="context">The context</param>
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
