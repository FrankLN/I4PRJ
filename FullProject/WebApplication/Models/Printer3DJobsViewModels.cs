using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class HistoryViewModel
    {
        public List<Printer3DJob> JobsInQueue { get; set; }
        public List<Printer3DJob> JobsInProgress { get; set; }
        public List<Printer3DJob> JobsDone { get; set; }

        public HistoryViewModel()
        {
            JobsInQueue = new List<Printer3DJob>();
            JobsInProgress = new List<Printer3DJob>();
            JobsDone = new List<Printer3DJob>();
        }
    }
    public class CreateViewModel
    {
        
    }
}