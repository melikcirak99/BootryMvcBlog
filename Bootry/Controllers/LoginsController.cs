using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bootry.Models;

namespace Bootry.Controllers
{
    public class LoginsController : Controller
    {
        VeriTabani db = new VeriTabani();

        [HttpGet]
        [Route("giris-{type}")]
        // GET: Logins
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("giris-{type}")]
        public ActionResult Login(string Mail, string Password)
        {
            if (Mail == null || Password == null)
            {
                ViewBag.message = "tüm bilgileri doldurun";
                return View("Login");
            }
            else
            {
                //admin için kontrol
                if (RouteData.Values["type"].ToString() == "admin")
                {
                    Admin _query = db.Admin.Where(x => x.Mail == Mail && x.Password == Password).FirstOrDefault();
                    if (_query != null)
                    {
                        //giriş başarılı
                        Session["admin"] = _query.ID.ToString();
                        return Redirect("/admins");
                    }
                    else
                    {
                        ViewBag.message = "0";
                        return View("Login");
                    }
                }

                //yazar için kontrol
                else if (RouteData.Values["type"].ToString() == "yazar")
                {
                    var _query = db.Writer.Where(x => x.Mail == Mail && x.Password == Password).FirstOrDefault();
                    if (_query != null)
                    {
                        //giriş başarılı
                        Session["yazar"] = _query.ID;
                        Session["yazar-bilgi"] = _query;
                        return Redirect("/Admins");
                    }
                    else
                    {
                        ViewBag.message = "0";
                        return View("Login");
                    }
                }

                //kulllanıcı için kontrol
                else if (RouteData.Values["type"].ToString() == "kullanici")
                {
                    var _query = db.User.Where(x => x.Mail == Mail && x.Password == Password).FirstOrDefault();
                    if (_query != null)
                    {
                        Session["user"] = _query.ID.ToString();
                        return Redirect("/Posts/Index");
                    }
                    else
                    {
                        ViewBag.message = "kullanıcı adı yada şifre hatalı";
                        return View("Login");
                    }
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }
    }
}