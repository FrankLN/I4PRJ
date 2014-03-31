using System.Runtime.Serialization;
using MessageTypes.Messages;

namespace MessageTypes
{
    public interface IServer
    {
        IMessage RecieveMessage();
        void SendToClient(ISerializable message);
        void RecieveFile(string fileName, long fileSize);
        void SendFile(string fileName, long fileSize);
    }
}
