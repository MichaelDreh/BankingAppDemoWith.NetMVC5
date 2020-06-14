using BankingSystem.Models;
using BankingSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankingSystem.Controllers
{
    [Authorize]
    public class TransferController : Controller
    {
        readonly ITransferService _service;

        public TransferController(ITransferService service)
        {
            _service = service;
        }

        // GET: Transfer

        public ActionResult Transfer()
        {
            return View();
        }
        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Transfer(TransferModel m)
        {
            if (ModelState.IsValid)
            {
                var result = _service.TransferTo(m);

                if (result)
                {
                    TempData.Add("Message", "Success");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData.Add("Message", "Transfer Failed");
                    return RedirectToAction("Index", "Home");
                }
            }
            
            return RedirectToAction("Index", "Home");
        }
        
        public ActionResult GetAcountDetails(long accountNumber)
        {
            var result = _service.GetCustomerAccountNumber(accountNumber);
            return Json(new { Name = result }, JsonRequestBehavior.AllowGet);
        }
    }
}