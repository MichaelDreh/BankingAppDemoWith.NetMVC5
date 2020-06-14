using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankingSystem.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Message(int statusCode)
        {
            ViewBag.statusCode = statusCode;
            return View();
        }
    }
}