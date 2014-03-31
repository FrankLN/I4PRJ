using System;
using System.Runtime.Serialization;
using Server;

namespace MessageTypes.Messages
{
    public interface IDownloadJobMsg
    {
        string FileName { get; }
    }
    [Serializable()]
    public class DownloadJobMsg : IMessage, ISerializable, IDownloadJobMsg
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
            serverApp.DownloadJob(this);
        }

        public void Run(IServerApp serverApp, IServer server)
        {
            serverApp.DownloadJob(this, server);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("FileName", FileName);
        }
    }
}