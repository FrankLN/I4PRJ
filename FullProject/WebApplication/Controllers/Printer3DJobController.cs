using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    /// <summary>
    /// The controller for all the sites about jobs.
    /// </summary>
    public class Printer3DJobController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Printer3DJob/
        /// <summary>
        /// The <c>Index</c> method gathers all the information
        /// to make the Job History site. This means getting data
        /// from the database and adding them to the HistoryViewModel.
        /// </summary>
        /// <returns>A HistoryViewModel for the Job History view</returns>
        [NewAuthorize(Roles = "Admin, User", NotifyUrl = "../Account/Activation")]
        public ActionResult Index()
        {
            HistoryViewModel jobTables = new HistoryViewModel();

            if (User.IsInRole("User"))
            {
                for (int i = 0; i < db.Printer3DJob.Count(); i++)
                {
                    if (db.Printer3DJob.ToList()[i].Owner == User.Identity.Name)
                    {
                        if (db.Printer3DJob.ToList()[i].Status == 0)
                        {
                            jobTables.JobsInQueue.Add(db.Printer3DJob.ToList()[i]);
                        }
                        else if (db.Printer3DJob.ToList()[i].Status == 1)
                        {
                            jobTables.JobsInProgress.Add(db.Printer3DJob.ToList()[i]);
                        }
                        else
                        {
                            jobTables.JobsDone.Add(db.Printer3DJob.ToList()[i]);
                        }
                    }
                }

                return View(jobTables);
            }

            for(int i = 0; i < db.Printer3DJob.Count(); i++)
            {
                if (db.Printer3DJob.ToList()[i].Status == 0)
                {
                    jobTables.JobsInQueue.Add(db.Printer3DJob.ToList()[i]);
                }
                else if (db.Printer3DJob.ToList()[i].Status == 1)
                {
                    jobTables.JobsInProgress.Add(db.Printer3DJob.ToList()[i]);
                }
                else
                {
                    jobTables.JobsDone.Add(db.Printer3DJob.ToList()[i]);
                }
            }

            return View(jobTables);
            //return View(db.Printer3DJob.ToList());
        }

        // GET: /Printer3DJob/Details/5
        [NewAuthorize(Roles = "Admin, User", NotifyUrl = "../Account/Activation")]
        [NewAuthorize(Roles = "Admin")]
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = new DetailsPrinterViewModel();
            Printer3DJob printer3djob = db.Printer3DJob.Find(id);
            PrintMaterial printmaterial = db.PrintMaterials.Find(4); //4 er kun i test øjemed (platic)

            model.Comment = printer3djob.Comment;
            model.CreationTime = printer3djob.CreationTime;
            model.Deadline = printer3djob.Deadline;
            model.Hollow = printer3djob.Hollow;
            model.Material = printmaterial;
            model.MyFile = printer3djob.MyFile;
            model.Owner = printer3djob.Owner;
            model.Printer3DJobId = printer3djob.Printer3DJobId;
            model.Status = printer3djob.Status;

            if (printer3djob == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

       //Download file

        /// <summary>
        /// Funktion til download af Job
        /// filenavn og filtype hentes fra database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>filenavn med mappen,filetypen, filnavn for at gemme</returns>

        [NewAuthorize(Roles = "Admin")]
        public FileResult DownloadFile (long? id)
        {
            Printer3DJob fileName = db.Printer3DJob.Find(id);

            string fName = fileName.MyFile;
            return File("~/App_Data/" + fName, System.Net.Mime.MediaTypeNames.Application.Octet,fName);
        }


       // GET: /Printer3DJob/Create
        [NewAuthorize(Roles = "Admin, User", NotifyUrl = "../Account/Activation")]
        public ActionResult Create()
        {
            CreateJobViewModel model = new CreateJobViewModel();

            return View();
        }

       
        /// <summary>
        /// POST: /Printer3DJob/Create
        ///  To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        /// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// </summary>
        /// <param name="printer3djob">The ViewModel</param>
        /// <param name="file">The file</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [NewAuthorize(Roles = "Admin, User", NotifyUrl = "../Account/Activation")]
        //public ActionResult Create(HttpPostedFileBase file)
        public ActionResult Create([Bind(Include = "Printer3DJobId,Owner,Deadline,MyFile,CreationTime,Hollow,Comment,Status, Material")] CreateJobViewModel printer3djob, HttpPostedFileBase file)
        {
            Printer3DJob p3Djob = new Printer3DJob();
            
            // Setting CreationTime to current date and time
            string CreateTime = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            p3Djob.CreationTime = CreateTime;

            // Setting the initial status to 0 (= job in queque)
            int initialStatus = 0;
            p3Djob.Status = initialStatus;

            // Owner is set to person logged in
            //ApplicationUser fUser = new ApplicationUser();
            //string fN = fUser.FName;
            p3Djob.Owner = (string) User.Identity.GetUserName();

            p3Djob.Comment = printer3djob.Comment;
            p3Djob.Deadline = printer3djob.Deadline;
            p3Djob.Hollow = printer3djob.Hollow;
            p3Djob.Material = printer3djob.Material;
            p3Djob.Printer3DJobId = printer3djob.Printer3DJobId;

            try
            {
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data"), fileName);

                    // Setting filename to name of chosen file and saving the name to database
                    string fName = (string)file.FileName;
                    p3Djob.MyFile = fName;

                    file.SaveAs(path);   
                }
                ViewBag.Message = "Upload successful";
                //return View(printer3djob);

                db.Printer3DJob.Add(p3Djob);
                db.SaveChanges();
                //return RedirectToAction("Index");
                //return View(printer3djob);

                return RedirectToAction("Index");

               
            }
            catch
            {
                ViewBag.Message = "Upload failed";
                return View(printer3djob);
            }
        }

        // GET: /Printer3DJob/Edit/5
       [NewAuthorize(Roles = "Admin, User", NotifyUrl = "../Account/Activation")]
        [NewAuthorize(Roles = "Admin")]
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Printer3DJob printer3djob = db.Printer3DJob.Find(id);
            if (printer3djob == null)
            {
                return HttpNotFound();
            }
            return View(printer3djob);
        }

        
        /// <summary>
        /// POST: /Printer3DJob/Edit/5
        /// o protect from overposting attacks, please enable the specific properties you want to bind to, for 
        /// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// </summary>
        /// <param name="printer3djob"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [NewAuthorize(Roles = "Admin, User", NotifyUrl = "../Account/Activation")]
        [NewAuthorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include="Printer3DJobId,Owner,Deadline,MyFile,CreationTime,Hollow,Comment,Status")] Printer3DJob printer3djob)
        {
            if (ModelState.IsValid)
            {
                db.Entry(printer3djob).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(printer3djob);
        }

        // GET: /Printer3DJob/Delete/5
        /// <summary>
        /// Returns the view from where the job can be deleted.
        /// </summary>
        /// <param name="id">The ID</param>
        /// <returns>The delete job view</returns>
        [NewAuthorize(Roles = "Admin, User", NotifyUrl = "../Account/Activation")]
        [NewAuthorize(Roles = "Admin")]
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Printer3DJob printer3djob = db.Printer3DJob.Find(id);
            if (printer3djob == null)
            {
                return HttpNotFound();
            }
            return View(printer3djob);
        }

        // POST: /Printer3DJob/Delete/5
        /// <summary>
        /// Deletes the job with the given ID.
        /// </summary>
        /// <param name="id">The ID of the job to be deleted</param>
        /// <returns>A view with the updated Job History</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [NewAuthorize(Roles = "Admin, User", NotifyUrl = "../Account/Activation")]
        [NewAuthorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(long id)
        {
            Printer3DJob printer3djob = db.Printer3DJob.Find(id);
            db.Printer3DJob.Remove(printer3djob);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
