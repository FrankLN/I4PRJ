using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseInterface
{
    public class JobClass
    {
        public MaterialClass Material { get; set; }
        public string Deadline { get; set; }
        public UserClass Owner { get; set; }
        public string File { get; set; }
        public string CreationTime { get; set; }
    }
}
