using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Server;

namespace MessageTypes.ReplyMessages
{
    public interface IDownloadJobReplyMsg
    {
        long FileSize { get; }
    }
    public class DownloadJobReplyMsg : IReplyMessage, ISerializable, IDownloadJobReplyMsg
    {
        public long FileSize { get; set; }

        public DownloadJobReplyMsg()
        {

        }

        public DownloadJobReplyMsg(SerializationInfo info, StreamingContext context)
        {
            FileSize = (long)info.GetValue("FileSize", typeof(long));
        }

        public void Run(IServerApp serverApp)
        {
            
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("FileSize", FileSize);
        }
    }
}