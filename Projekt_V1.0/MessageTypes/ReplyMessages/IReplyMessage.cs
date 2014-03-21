using ConsoleApplication1;

namespace MessageTypes.ReplyMessages
{
    public interface IReplyMessage
    {
        void Run(IClientCmd clientCmd);
    }
}