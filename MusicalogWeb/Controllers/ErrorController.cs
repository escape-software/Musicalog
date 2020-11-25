using MusicalogWeb.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicalogWeb.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Error500()
        {
            TempData["AppTitle"] = Settings.Default.AppTitle;
            TempData["ErrorMessage"] = HttpContext.Application["ErrorMessage"] != null ? HttpContext.Application["ErrorMessage"] : "";

            return View("Error500", "_LayoutError");
        }
    }
}