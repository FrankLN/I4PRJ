using Server;

namespace MessageTypes.Messages
{
    public interface IMessage
    {
        void Run(IServerApp serverApp);
    }
}