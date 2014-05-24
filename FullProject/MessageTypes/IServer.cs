using System.Runtime.Serialization;
using MessageTypes.Messages;

namespace MessageTypes
{
    /// <summary>
    /// Interface for <c>Server</c>.
    /// </summary>
    public interface IServer
    {
        /// <summary>
        /// Method signature for method <c>RecieveMessage</c>.
        /// </summary>
        /// <returns>The recieved message.</returns>
        IMessage RecieveMessage();

        /// <summary>
        /// Method signature for method <c>SendToClient</c>.
        /// </summary>
        /// <param name="message">The reply message for the client.</param>
        void SendToClient(ISerializable message);

        /// <summary>
        /// Method signature for method <c>RecieveFile</c>.
        /// </summary>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="fileSize">The size of the file.</param>
        void RecieveFile(string fileName, long fileSize);

        /// <summary>
        /// Method signature for method <c>SendFile</c>.
        /// </summary>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="fileSize">The size of the file.</param>
        void SendFile(string fileName, long fileSize);
    }
}
