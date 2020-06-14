using BankingSystem.Models;
using BankingSystem.Resources.Context;
using BankingSystem.Resources.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingSystem.Services
{
    public class CustomerDetailsService
    {
        public Customer Details(string loginuser)
        {
            var result = new Customer();
            using(var db = new BankingContext())
            {
                var logincustomer = db.Customers.FirstOrDefault(a => a.Email == loginuser);
               
                result = logincustomer;
            }

            return result;
        }

        public CustomResponse ContactUs(ContactUsModel model)
        {
            try
            {
                EmailService.SendEmail(model.Email, model.Subject, model.Message, true);

                return new CustomResponse()
                {
                    IsSent = true,
                    Message = "Message Successfully Sent We Will Get Back To You Soon",
                };
            }
            catch (Exception)
            {
                return new CustomResponse()
                {
                    IsSent = false,
                    Message = "Failed"
                };
            }
        }
    }
}