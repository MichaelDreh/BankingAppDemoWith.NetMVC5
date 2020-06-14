using BankingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BankingSystem.Controllers
{
    
    public class AccountController : Controller
    {
        readonly Services.LoginService _service;

        public AccountController()
        {
            _service = new Services.LoginService();
        }

        // GET: Account
       public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(new LoginModel());

        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel m)
        {
            var result = _service.Login(m.UserName, m.Password);
            if (result)
            {
                FormsAuthentication.SetAuthCookie(m.UserName,false);
                return RedirectToAction("Index", "Home");
            }

            else
            {
                return View(m);
            }            
        }

       [HttpPost,ValidateAntiForgeryToken]
       public ActionResult Register(RegisterModel m)
        {
           
            if (ModelState.IsValid)
            {
                var result = _service.Register(m);

                if (result)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                _ = _service.ResetPassword(model.Email, out string message);
                ViewBag.Message = message;
            }
            return View();
        }

        public ActionResult ResetPassword(string g)
        {
            try
            {
                if (_service.IsCodeValid(g, out string message))
                {
                    var bytes = Convert.FromBase64String(g);
                    var decoded = Encoding.UTF8.GetString(bytes);
                    var email = decoded.Split(new char[] { '!' }, StringSplitOptions.RemoveEmptyEntries)[0];

                    return View(new ResetPasswordModel{ Email = email });
                }

                TempData["Message"] = message;
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            var result = _service.ConfirmResetPassword(model);

            if (result)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View(model);
            }
        }


    }


}