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
        /// <summary>
        /// Index which show a list of print materials
        /// </summary>
        /// <returns>A list of print materials</returns>
        [NewAuthorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.PrintMaterials.ToList());
        }

        // GET: /PrintMaterial/Details/5
        /// <summary>
        /// Show details about a print material
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns>A printmaterial</returns>
         [NewAuthorize(Roles = "Admin")]
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

        [NewAuthorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /PrintMaterial/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Create new material method
        /// POST: /PrintMaterial/Create
        /// </summary>
        /// <param name="printmaterial"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [NewAuthorize(Roles = "Admin")]
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
        [NewAuthorize(Roles = "Admin")]
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
        /// <summary>
        /// Edit method
        ///  POST: /PrintMaterial/Edit/5
        /// </summary>
        /// <param name="printmaterial"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Delete material method
        /// GET: /PrintMaterial/Delete/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [NewAuthorize(Roles = "Admin")]
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
        /// <summary>
        /// Delete confiremd method
        /// POST: /PrintMaterial/Delete/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [NewAuthorize(Roles = "Admin")]
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
