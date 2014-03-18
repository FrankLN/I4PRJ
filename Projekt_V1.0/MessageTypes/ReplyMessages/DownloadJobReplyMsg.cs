using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Server;

namespace MessageTypes.ReplyMessages
{
    public interface IDownloadJobReplyMsg
    {
         List<byte> File3D 
    }
    public class DownloadJobReplyMsg : IReplyMessage, ISerializable, IDownloadJobReplyMsg
    {
        public List<byte> File3D { get; set; }

        public DownloadJobReplyMsg()
        {

        }

        public DownloadJobReplyMsg(SerializationInfo info, StreamingContext context)
        {
            File3D = (List<byte>)info.GetValue("Email", typeof(List<byte>));

        }

        public void Run(IServerApp serverApp)
        {
            
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("File3D", File3D);
        }
    }
}