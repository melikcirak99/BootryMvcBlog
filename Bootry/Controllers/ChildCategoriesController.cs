using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bootry.Models;

namespace Bootry.Controllers
{
    public class ChildCategoriesController : Controller
    {
        private VeriTabani db = new VeriTabani();

        // GET: ChildCategories
        public ActionResult Index()
        {
            var childCategories = db.ChildCategories.Include(c => c.Category);
            return View(childCategories.ToList());
        }

        // GET: ChildCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChildCategories childCategories = db.ChildCategories.Find(id);
            if (childCategories == null)
            {
                return HttpNotFound();
            }
            return View(childCategories);
        }

        // GET: ChildCategories/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Category, "ID", "Name");
            return View();
        }

        // POST: ChildCategories/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CategoryID,Name")] ChildCategories childCategories)
        {
            if (ModelState.IsValid)
            {
                db.ChildCategories.Add(childCategories);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Category, "ID", "Name", childCategories.CategoryID);
            return View(childCategories);
        }

        // GET: ChildCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChildCategories childCategories = db.ChildCategories.Find(id);
            if (childCategories == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Category, "ID", "Name", childCategories.CategoryID);
            return View(childCategories);
        }

        // POST: ChildCategories/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CategoryID,Name")] ChildCategories childCategories)
        {
            if (ModelState.IsValid)
            {
                db.Entry(childCategories).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Category, "ID", "Name", childCategories.CategoryID);
            return View(childCategories);
        }

        // GET: ChildCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChildCategories childCategories = db.ChildCategories.Find(id);
            if (childCategories == null)
            {
                return HttpNotFound();
            }
            return View(childCategories);
        }

        // POST: ChildCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChildCategories childCategories = db.ChildCategories.Find(id);
            db.ChildCategories.Remove(childCategories);
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
