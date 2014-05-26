using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    /// <summary>
    /// <c>HistoryViewModel</c> contains three lists of Printer3DJobs.
    /// </summary>
    public class HistoryViewModel
    {
        /// <summary>
        /// The property <c>JobsInQueue</c> is a list containing all the jobs in queue.
        /// </summary>
        public List<Printer3DJob> JobsInQueue { get; set; }

        /// <summary>
        /// The property <c>JobsInProgress</c> is a list containing all the jobs in progress.
        /// </summary>
        public List<Printer3DJob> JobsInProgress { get; set; }

        /// <summary>
        /// The property <c>JobsDone</c> is a list containing all the jobs which are done.
        /// </summary>
        public List<Printer3DJob> JobsDone { get; set; }

        /// <summary>
        /// The default <c>HistoryViewModel</c> constructor.
        /// </summary>
        public HistoryViewModel()
        {
            JobsInQueue = new List<Printer3DJob>();
            JobsInProgress = new List<Printer3DJob>();
            JobsDone = new List<Printer3DJob>();
        }
    }
    public class CreateJobViewModel
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

    public class DetailsPrinterViewModel
    {
        [Display(Name = "Printer 3D Job Id")] public long Printer3DJobId;

        [Display(Name = "Owner")] public string Owner;

        [Display(Name = "Deadline")] public string Deadline;

        [Display(Name = "MyFile")] public string MyFile;

        [Display(Name = "Time of creation")] public string CreationTime;

        [Display(Name = "Hollow")] public int Hollow;

        [Display(Name = "Comment")] public string Comment;

        [Display(Name = "Print status")] public int Status;

        [Display(Name = "Print material")] public PrintMaterial Material;
    }
}