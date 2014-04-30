using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class Printer3DJobController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Printer3DJob/
        public ActionResult Index()
        {
            return View(db.Printer3DJob.ToList());
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

        // GET: /Printer3DJob/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Printer3DJob/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Printer3DJobId,Owner,Deadline,MyFile,CreationTime,Hollow,Comment,Status")] Printer3DJob printer3djob)
        {
            if (ModelState.IsValid)
            {
                db.Printer3DJob.Add(printer3djob);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(printer3djob);
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
