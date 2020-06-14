using BankingSystem.Models;
using BankingSystem.Resources.Entities;
using BankingSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BankingSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        readonly CustomerDetailsService _service;
        private readonly ITransferService _transaction;

        public HomeController(ITransferService transaction)
        {
            _transaction = transaction;
            _service = new CustomerDetailsService();
            //_transaction = new Services.TransferService();
        }

        public ActionResult Index()
        {
            var transactions = _transaction.Transactions();
            ViewBag.Transactions = transactions;

            var loginuser = User.Identity.Name;

            ViewBag.Message = TempData["Message"];
            var currentusername = _transaction.GetCurrentUserName(loginuser);

            ViewBag.FirstName = currentusername;

            var result = _service.Details(loginuser);

            ViewBag.userDetails = result;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.Email = User.Identity.Name;
            return View();
        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Contact(ContactUsModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _service.ContactUs(model);
                ViewBag.Message = result.Message;

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            HttpCookie httpCookie = new HttpCookie("Cookie1", "")
            {
                Expires = DateTime.Now.AddYears(-1)
            };
            Response.Cookies.Add(httpCookie);
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

    }
}
