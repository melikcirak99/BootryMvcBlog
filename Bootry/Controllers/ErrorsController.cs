using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bootry.Controllers
{
    public class ErrorsController : Controller
    {
        // GET: Errors
        public ActionResult ERR404()
        {
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;
            return View();
        }
    }
}