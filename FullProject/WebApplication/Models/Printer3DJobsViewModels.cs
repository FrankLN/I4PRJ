using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

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

        [Display(Name = "Printer 3D Job Id")]
        public long Printer3DJobId { get; set; }

        [Required(ErrorMessage = "The field Ownener must be filled in.")]
        [Display(Name = "Owner")]
        public string Owner { get; set; }

        [Display(Name = "Deadline")]
        [DataType(DataType.Date)]
        public string Deadline { get; set; }

        [Required(ErrorMessage = "The field File must be filled in.")]
        [Display(Name = "MyFile")]
        [DataType(DataType.Upload)]
        [NotMapped]
        public HttpPostedFileBase MyFile { get; set; }

        [Display(Name = "Time of creation")]
        public string CreationTime { get; set; }

        [Display(Name = "Hollow")]
        public int Hollow { get; set; }

        [Display(Name = "Comment")]
        public string Comment { get; set; }

        [Display(Name = "Print status")]
        public int Status { get; set; }

        [Display(Name = "Print material")]
        public PrintMaterial Material { get; set; }
    }

}