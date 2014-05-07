using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class Printer3DJob
    {
        public long Printer3DJobId { get; set; }
        public string Owner { get; set; }
        public string Deadline { get; set; }
        public string MyFile { get; set; }
        public string CreationTime { get; set; }
        public int Hollow { get; set; }
        public string Comment { get; set; }
        public int Status { get; set; }
    }
}