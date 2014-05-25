using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseInterface
{
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

        public MaterialClass()
        {
            MaterialId = 0;
            MaterialType = "";
        }

        public MaterialClass(SerializationInfo info, StreamingContext context)
        {
            MaterialId = (int)info.GetValue("MaterialId", typeof(int));
            MaterialType = (string) info.GetValue("MaterialType", typeof (string));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("MaterialId", MaterialId);
            info.AddValue("MaterialType", MaterialType);
        }

        public override string ToString()
        {
            return MaterialType;
        }
    }
}
