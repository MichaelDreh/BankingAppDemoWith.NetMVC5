using BankingSystem.Models;
using BankingSystem.Resources.Context;
using BankingSystem.Resources.Entities;
using PagedList.Mvc;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace BankingSystem.Services
{
    public interface ITransferService
    {
        bool TransferTo(TransferModel model);
        List<Transaction> Transactions();
        string GetCustomerAccountNumber(long accountNumber);
        string GetCurrentUserName(string currentusername);
    }



    public class TransferService: ITransferService
    {
        public BankingContext context;

        public TransferService(BankingContext context)
        {
            this.context = context;
        }

        public bool TransferTo(TransferModel model)
        {
            StringBuilder stringBuilder = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < 8; i++)
            {
                stringBuilder.Append(random.Next(0, 9));
            }

            var result = false;
            var currentusername = HttpContext.Current.User.Identity.Name;
            int currentuserid;
            try
            {
                    var charges = 10;
                    
                    currentuserid = context.Customers.FirstOrDefault(a => a.Email == currentusername).Id;
                    string receivername = context.Customers.FirstOrDefault(a => a.AccountNumber == model.AcountNumber).FirstName;
                    var sender =  context.Customers.Where(a => a.Id == currentuserid).FirstOrDefault();
                    var receiver = context.Customers.Where(a => a.FirstName == receivername).FirstOrDefault();

                    if (sender.Balance >= model.Amount && sender.AccountNumber != receiver.AccountNumber)
                    {
                        var transaction = new Transaction()
                        {
                            Amount = model.Amount,
                            Channel = "Mobile Transfer",
                            TransactionId = Convert.ToString(stringBuilder),
                            ReceiverName = receivername,
                            Charges = charges,
                            TransDate = DateTime.Now,
                            TransType = "Debit",
                            CustomerId = currentuserid
                        };
                        sender.Balance -= (model.Amount + charges);
                        receiver.Balance += model.Amount;
                        context.Transactions.Add(transaction);
                        using (MailMessage mailMessage = new MailMessage())
                        {
                            mailMessage.To.Add(sender.Email);
                            mailMessage.From = new MailAddress("ajibademichael30@gmail.com");
                            mailMessage.Subject = string.Format("Debit Alert");
                            mailMessage.Body = string.Format("<h3>Dear {0}!!</h3> <h4>You have successfully made a tranfer of {1} to {2}</h4>", sender.FirstName, model.Amount, receivername);
                            mailMessage.IsBodyHtml = true;
                            SmtpClient smtp = new SmtpClient
                            {
                                Host = "smtp.gmail.com",
                                Port = 587,
                                UseDefaultCredentials = false,
                                Credentials = new System.Net.NetworkCredential("email", "password"),
                                EnableSsl = true
                            };
                            smtp.Send(mailMessage);
                        }
                        result = context.SaveChanges() > 0;
                    }
                    
            }
            catch(Exception ex)
            {
                var message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
            return result;
        }

        public List<Transaction> Transactions()
        {
            var result = new List<Transaction>();
            var currentuser = HttpContext.Current.User.Identity.Name;
            try
            {
                using(var db = new BankingContext())
                {
                    var userid = db.Customers.FirstOrDefault(a => a.Email == currentuser).Id;
                    var currendusername = db.Customers.FirstOrDefault(a => a.Email == currentuser).FirstName;
                    var totalrecords = db.Transactions.Where(a => a.CustomerId == userid || a.ReceiverName == currendusername).ToList().Count();
                    result = db.Transactions.Where(a => a.CustomerId == userid || a.ReceiverName == currendusername).OrderBy(a => a.Id).Skip(totalrecords - 10).Take(10).ToList();
                    var paged = result.Skip(3).Take(3).ToList();
                    //var list = db.Customers.ToList();
                    //var paged = list.Skip((2 - 1) * 10).Take(10).ToList();
                    //var betterPaged = db.Customers.Skip(10).Take(10).ToList();
                }
            }
            catch(Exception ex)
            {

            }

            return result;
        }

       public string GetCustomerAccountNumber(long accountNumber)
       {
            using(var db = new BankingContext())
            {
                return db.Customers.FirstOrDefault(a => a.AccountNumber == accountNumber)?.FirstName;
            }
       }

        public string GetCurrentUserName(string currentusername)
        {
            using (var db = new BankingContext())
            {
                return db.Customers.FirstOrDefault(a => a.Email == currentusername)?.FirstName;
            }
        }
    }
}