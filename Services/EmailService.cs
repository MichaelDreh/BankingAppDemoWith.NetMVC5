using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace BankingSystem.Services
{
    public static class EmailService
    {
        public static void SendEmail(string address, string subject, string body, bool isHtml)
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.To.Add(address);
                mailMessage.From = new MailAddress("email");
                mailMessage.Subject = string.Format(subject);
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = isHtml;
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
        }
    }
}