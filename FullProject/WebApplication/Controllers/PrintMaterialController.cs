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
    public class PrintMaterialController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /PrintMaterial/
        public ActionResult Index()
        {
            return View(db.PrintMaterials.ToList());
        }

        // GET: /PrintMaterial/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrintMaterial printmaterial = db.PrintMaterials.Find(id);
            if (printmaterial == null)
            {
                return HttpNotFound();
            }
            return View(printmaterial);
        }

        // GET: /PrintMaterial/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /PrintMaterial/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="PrintMaterialId,MaterialType")] PrintMaterial printmaterial)
        {
            if (ModelState.IsValid)
            {
                db.PrintMaterials.Add(printmaterial);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(printmaterial);
        }

        // GET: /PrintMaterial/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrintMaterial printmaterial = db.PrintMaterials.Find(id);
            if (printmaterial == null)
            {
                return HttpNotFound();
            }
            return View(printmaterial);
        }

        // POST: /PrintMaterial/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="PrintMaterialId,MaterialType")] PrintMaterial printmaterial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(printmaterial).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(printmaterial);
        }

        // GET: /PrintMaterial/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrintMaterial printmaterial = db.PrintMaterials.Find(id);
            if (printmaterial == null)
            {
                return HttpNotFound();
            }
            return View(printmaterial);
        }

        // POST: /PrintMaterial/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PrintMaterial printmaterial = db.PrintMaterials.Find(id);
            db.PrintMaterials.Remove(printmaterial);
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
