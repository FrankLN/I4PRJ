namespace MessageTypes.ReplyMessages
{
    /// <summary>
    /// Interface for all reply messages.
    /// Client can handle all messages the same way because of this.
    /// </summary>
    public interface IReplyMessage
    {
        /// <summary>
        /// Method signature for Method Run.
        /// </summary>
        /// <param name="clientCmd">The <c>ClientCmd</c> needed for calling a method</param>
        void Run(IClientCmd clientCmd);
    }
}