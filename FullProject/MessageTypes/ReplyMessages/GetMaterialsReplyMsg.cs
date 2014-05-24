using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DatabaseInterface;

namespace MessageTypes.ReplyMessages
{
    /// <summary>
    /// Interface for <c>GetMaterialsReplyMsg</c>.
    /// </summary>
    public interface IGetMaterialsReplyMsg
    {
        /// <summary>
        /// The property <c>Materials</c> for encapsulating.
        /// </summary>
        List<MaterialClass> Materials { get; }
    }

    /// <summary>
    /// The message which replies a <c>GetMaterialsMsg</c>
    /// </summary>
    [Serializable()]
    public class GetMaterialsReplyMsg : IReplyMessage, ISerializable, IGetMaterialsReplyMsg
    {
        /// <summary>
        /// The property <c>Materials</c> is a list of the materials the <c>Server</c> reply to the <c>Client</c>.
        /// </summary>
        public List<MaterialClass> Materials { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetMaterialsReplyMsg()
        {

        }

        /// <summary>
        /// Explicit constructor for Serializing.
        /// </summary>
        /// <param name="info">For Serializing.</param>
        /// <param name="context">For Serializing.</param>
        public GetMaterialsReplyMsg(SerializationInfo info, StreamingContext context)
        {
            Materials = (List<MaterialClass>)info.GetValue("Materials", typeof(List<MaterialClass>));

        }

        /// <summary>
        /// Runs the method <c>LoadMaterials</c> on the clientCmd.
        /// </summary>
        /// <param name="clientCmd">The clientCmd which has requested the server.</param>
        public void Run(IClientCmd clientCmd)
        {
           clientCmd.LoadMaterials(this);
        }

        /// <summary>
        /// Used for serializing.
        /// </summary>
        /// <param name="info">Used for serializing.</param>
        /// <param name="context">Used for serializing.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Materials", Materials);
        }
    }
}