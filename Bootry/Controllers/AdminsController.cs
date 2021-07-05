using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bootry.Models;
using Bootry.Controllers.Base;

namespace Bootry.Controllers
{
    public class AdminsController : SiteBaseController
    {
        private VeriTabani db = new VeriTabani();

        // GET: Admins
        public ActionResult Index()
        {
            if (this.IsAdminLogin || this.IsWriterLogin)
            {
                ViewBag.Posts = db.Post.Where(x => x.isActive == true).ToList();
                ViewBag.Categories = db.Category.ToList();
                ViewBag.Writers = db.Writer.ToList();
                ViewBag.trashCount = db.Post.Where(x => x.isActive == false).ToList().Count;
                return View(db.Admin.ToList());
            }
            return Redirect("/giris-yazar");
        }


        [HttpGet]
        [Route("gonderi-sil/{id}")]
        public ActionResult Delete(int? id)
        {
            if (this.IsAdminLogin || this.IsWriterLogin)
            {
                if (id == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    try
                    {
                        Post post = db.Post.Find(id);
                        db.Post.Remove(post);
                        db.SaveChanges();
                        return RedirectToAction("Trash", new { message = "1" });
                    }
                    catch
                    {
                        return RedirectToAction("Trash", new { message = "0" });
                    }
                }

            }
            return Redirect("/giris-yazar");
        }


        // GET: Admins/Details/5
        public ActionResult Details(int? id)
        {
            if (this.IsAdminLogin || this.IsWriterLogin)
            {
                if (id == null)
                {
                    return HttpNotFound();
                }
                Admin admin = db.Admin.Find(id);
                if (admin == null)
                {
                    return HttpNotFound();
                }
                return View(admin);
            }
            return Redirect("/giris-yazar");

        }

        // GET: Admins/Create
        public ActionResult Create()
        {
            if (this.IsAdminLogin)
            {
                return View();
            }
            return Redirect("/giris-yazar");

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,LastName,Mail,Username,Password")] Admin admin)
        {
            if (this.IsAdminLogin)
            {
                if (ModelState.IsValid)
                {
                    db.Admin.Add(admin);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(admin);
            }
            return Redirect("giris-admin");
        }

        // GET: Admins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (this.IsAdminLogin)
            {
                if (id == null)
                {
                    return HttpNotFound();
                }
                Admin admin = db.Admin.Find(id);
                if (admin == null)
                {
                    return HttpNotFound();
                }
                return View(admin);
            }
            return Redirect("giris-admin");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,LastName,Mail,Username,Password")] Admin admin)
        {
            if (this.IsAdminLogin)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(admin).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(admin);
            }
            return Redirect("giris-admin");
        }

        public ActionResult Trash(string message = "")
        {
            if (this.IsAdminLogin)
            {
                ViewBag.message = message;
                return View();
            }
            return Redirect("/giris-yazar");
        }


        [HttpGet]
        public ActionResult Settings()
        {
            if (this.IsAdminLogin)
            {
                ViewBag.CategoryID = new SelectList(db.Category, "ID", "Name");
                ViewBag.nowSiteName = db.Settings.Find(2).Value.ToString();
                ViewBag.nowSiteDescription = db.Settings.Find(3).Value.ToString();
                int nowCateID = Convert.ToInt32(db.Settings.Find(1).Value);
                ViewBag.nowSelectedCat = db.Category.Find(nowCateID);
                ViewBag.allCategories = db.Category.ToList();
                var model = db.Settings.ToList();
                return View(model);
            }
            return Redirect("/giris-yazar");
        }

        public ActionResult Comments()
        {
            if (this.IsAdminLogin)
            {
                return View();
            }
            return Redirect("/posts");
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
