using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class ErrorsController : Controller
    {
        // GET: Errors
        public ActionResult Index()
        {
            return View("~/Views/Errors/Details.cshtml");
        }

        public ActionResult Code(int? id)
        {
            switch (id)
            {
                case 404:
                    return View("~/Views/Errors/Missing.cshtml");
                default:
                    return View("~/Views/Errors/Details.cshtml");
            }
        }
    }
}