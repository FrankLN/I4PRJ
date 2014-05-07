﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class Printer3DJobController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Printer3DJob/
        public ActionResult Index()
        {
            HistoryViewModel jobTables = new HistoryViewModel();

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
        public ActionResult Details(long? id)
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

        public FileResult DownloadFile (long? id)
        {
            Printer3DJob fileName = db.Printer3DJob.Find(id);

            string fName = fileName.MyFile;
            string path = Environment.CurrentDirectory;
            return File("~/App_Data/" + fName, System.Net.Mime.MediaTypeNames.Application.Octet,fName);
        }

        // GET: /Printer3DJob/Create
        public ActionResult Create()
        {
            Printer3DJob model = new Printer3DJob();

            return View();
        }

        // POST: /Printer3DJob/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create(HttpPostedFileBase file)
        public ActionResult Create([Bind(Include = "Printer3DJobId,Owner,Deadline,MyFile,CreationTime,Hollow,Comment,Status")] Printer3DJob printer3djob, HttpPostedFileBase file)
        {
            //if (ModelState.IsValid)
            //{

            string fName = (string)file.FileName;
            printer3djob.MyFile = fName;

            db.Printer3DJob.Add(printer3djob);
            db.SaveChanges();
            //return RedirectToAction("Index");
            //return View(printer3djob);

            
            try
            {
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data"), fileName);
                    file.SaveAs(path);
                }
                ViewBag.Message = "Upload successful";
                return View(printer3djob);
            }
            catch
            {
                ViewBag.Message = "Upload failed";
                return View(printer3djob);
            }
        }

        // GET: /Printer3DJob/Edit/5
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

        // POST: /Printer3DJob/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
