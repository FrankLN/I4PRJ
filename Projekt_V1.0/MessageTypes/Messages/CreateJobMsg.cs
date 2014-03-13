using System.Runtime.Serialization;
using Server;

namespace MessageTypes.Messages
{
    public class CreateJobMsg : IMessage, ISerializable
    {
        public string Material { get; set; }
        public bool Hollow { get; set; }
        public string Date { get; set; }
        public string FileName { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }

        public CreateJobMsg()
        {

        }

        public CreateJobMsg(SerializationInfo info, StreamingContext context)
        {
            Material = (string)info.GetValue("Material", typeof(string));
            Hollow = (bool)info.GetValue("Hollow", typeof(bool));
            Date = (string)info.GetValue("Date", typeof(string));
            FileName = (string)info.GetValue("FileName", typeof(string));
            Email = (string)info.GetValue("Email", typeof(string));
            Comment = (string)info.GetValue("Comment", typeof(string));
        }

        public void Run(IServerApp serverApp)
        {
            serverApp.CreateJob(Material, Hollow, Date, FileName, Email, Comment);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Material", typeof(string));
            info.AddValue("Hollow", typeof(bool));
            info.AddValue("Date", typeof(string));
            info.AddValue("FileName", typeof(string));
            info.AddValue("Email", typeof(string));
            info.AddValue("Comment", typeof(string));
        }
    }
}