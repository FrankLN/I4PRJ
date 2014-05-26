using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseInterface
{
    /// <summary>
    /// <c>MaterialClass</c> is the class version of the table Material.
    /// </summary>
    [Serializable()]
    public class MaterialClass : ISerializable
    {
        /// <summary>
        /// The property <c>MaterialId</c>
        /// </summary>
        public int MaterialId { get; set; }

        /// <summary>
        /// The property <c>MaterialType</c>
        /// </summary>
        public string MaterialType { get; set; }

        /// <summary>
        /// The default <c>MaterialClass</c> constructor.
        /// </summary>
        public MaterialClass()
        {
            MaterialId = 0;
            MaterialType = "";
        }

        /// <summary>
        /// The explicit <c>MaterialClass</c> constructor.
        /// </summary>
        /// <param name="info">The material's data</param>
        /// <param name="context">The context</param>
        public MaterialClass(SerializationInfo info, StreamingContext context)
        {
            MaterialId = (int)info.GetValue("MaterialId", typeof(int));
            MaterialType = (string) info.GetValue("MaterialType", typeof (string));
        }

        /// <summary>
        /// <c>GetObjectData</c> gets the material's data.
        /// </summary>
        /// <param name="info">A container for the material's data</param>
        /// <param name="context">The context</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("MaterialId", MaterialId);
            info.AddValue("MaterialType", MaterialType);
        }


        /// <summary>
        /// <c>ToString</c> returns the material class as a string.
        /// </summary>
        /// <returns>The material type</returns>
        public override string ToString()
        {
            return MaterialType;
        }
    }
}
