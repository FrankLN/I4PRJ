using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseInterface
{
    public class JobClass
    {
        public JobClass()
        {
            OrderId = 0;
            Material = null;
            Deadline = null;
            Owner = null;
            MyFile = null;
            CreationTime = null;
            Hollow = 0;             // Hollow is set to 0 as the 3DJob is full by default.
            Comment = null;
        }

        public int OrderId { get; set; }
        public MaterialClass Material { get; set; }
        public string Deadline { get; set; }
        public UserClass Owner { get; set; }
        public string MyFile { get; set; }
        public string CreationTime { get; set; }
        public int Hollow { get; set; }
        public string Comment { get; set; }
    }
}
