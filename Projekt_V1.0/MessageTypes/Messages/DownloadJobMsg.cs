using System.Runtime.Serialization;
using Server;

namespace MessageTypes.Messages
{
    public class DownloadJobMsg : IMessage, ISerializable
    {
        public string FileName { get; set; }

        public DownloadJobMsg()
        {

        }

        public DownloadJobMsg(SerializationInfo info, StreamingContext context)
        {
            FileName = (string)info.GetValue("FileName", typeof(string));
        }

        public void Run(IServerApp serverApp)
        {
            serverApp.DownloadJob(FileName);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("FileName", FileName);
        }
    }
}