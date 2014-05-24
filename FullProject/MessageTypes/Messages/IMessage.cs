namespace MessageTypes.Messages
{
    public interface IMessage
    {
        void Run(IServerApp serverApp, IServer server);
    }
}