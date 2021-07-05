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
    public class CategoriesController : Controller
    {
        private VeriTabani db = new VeriTabani();

        // GET: Categories
        public ActionResult Index()
        {
            if (Session["admin"] != null)
            {
                return View(db.Category.ToList());
            }
            return HttpNotFound();
        }

        //partial view kategoriler
        public PartialViewResult Kategoriler()
        {
            if (Session["admin"] != null)
            {
                var model = db.Category.ToList();
                return PartialView(model);
            }
            return PartialView();

        }



        // GET: Categories/Create
        public ActionResult Create()
        {
            if (Session["admin"] != null)
            {
                return View();
            }
            return HttpNotFound();

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] Category category)
        {
            if (Session["admin"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.Category.Add(category);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(category);
            }
            return HttpNotFound();


        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["admin"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Category category = db.Category.Find(id);
                if (category == null)
                {
                    return HttpNotFound();
                }
                return View(category);
            }
            return HttpNotFound();


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] Category category)
        {
            if (Session["admin"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(category).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(category);
            }
            return HttpNotFound();


        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["admin"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Category category = db.Category.Find(id);
                if (category == null)
                {
                    return HttpNotFound();
                }
                return View(category);
            }
            return HttpNotFound();


        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["admin"] != null)
            {
                Category category = db.Category.Find(id);
                db.Category.Remove(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound();


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
