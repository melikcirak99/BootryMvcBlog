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
    public class UsersController : SiteBaseController
    {
        private VeriTabani db = new VeriTabani();
        public ActionResult Index()
        {
            if (this.IsAdminLogin)
            {
                var model = db.User.ToList();
                return View(model);
            }
            return Redirect("/giris-yazar");
        }
        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (this.IsAdminLogin)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                User user = db.User.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
            return Redirect("/giris-yazar");
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {

            if (ModelState.IsValid)
            {
                var _sorgu = db.User.Where(x => x.Mail == user.Mail).FirstOrDefault();
                if (_sorgu != null)
                {
                    //kullanıcı zaten eklenmiş
                    ViewBag.message = "bu mail ile zaten kayıt yapılmış";
                    return View(user);
                }
                var _query = db.User.Where(x => x.Mail == user.Mail).FirstOrDefault();
                if (_query != null)
                {
                    ViewBag.message = "0";
                    return View(user);
                }
                user.isApproved = true;
                db.User.Add(user);
                db.SaveChanges();
                ViewBag.message = "1";
                return View();

            }
            else
            {
                ViewBag.message = "0";
                return View(user);
            }

        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (this.IsAdminLogin)
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                User user = db.User.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
            return Redirect("/giris-kullanici");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,LastName,Mail,Password,isApproved")] User user)
        {
            if (this.IsUserLogin)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(user);
            }
            return Redirect("/giris-kullanici");
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (this.IsAdminLogin || this.IsUserLogin)
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                User user = db.User.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
            return Redirect("/giris-kullanici");
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (this.IsAdminLogin || this.IsUserLogin)
            {

                User user = db.User.Find(id);
                db.User.Remove(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return Redirect("/giris-kullanici");
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
