using Bootry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Bootry.Controllers.Base
{
    public class SiteBaseController : Controller
    {
        public bool IsAdminLogin { get; private set; }
        public bool IsWriterLogin { get; set; }
        public bool IsUserLogin { get; set; }

        public int AdminID { get; private set; }
        public int WriterID { get; private set; }
        public int UserID { get; private set; }


        protected override void Initialize(RequestContext requestContext)
        {
            if (requestContext.HttpContext.Session["admin"] != null)
            {
                IsAdminLogin = true;
                AdminID = Convert.ToInt32(requestContext.HttpContext.Session["admin"]);
            }
            if (requestContext.HttpContext.Session["yazar"] != null)
            {
                IsWriterLogin = true;
                WriterID = Convert.ToInt32(requestContext.HttpContext.Session["yazar"]);
            }
            if (requestContext.HttpContext.Session["user"] != null)
            {
                IsUserLogin = true;
                UserID = Convert.ToInt32(requestContext.HttpContext.Session["user"]);
            }
            base.Initialize(requestContext);
        }
    }
}