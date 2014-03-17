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
            File = null;
            CreationTime = null;
        }

        public int OrderId { get; set; }
        public MaterialClass Material { get; set; }
        public string Deadline { get; set; }
        public UserClass Owner { get; set; }
        public string File { get; set; }
        public string CreationTime { get; set; }
    }
}
