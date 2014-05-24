namespace MessageTypes.Messages
{
    /// <summary>
    /// Interface for all messages. The <c>Server</c> is able 
    /// to handle all messages the same way because of this.
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// The method <c>Run</c> is the method signature which the <c>Server</c> runs upon recive.
        /// </summary>
        /// <param name="serverApp">The <c>ServerApp</c> which runs the message.</param>
        /// <param name="server">The <c>Server</c> which recieves the message.</param>
        void Run(IServerApp serverApp, IServer server);
    }
}