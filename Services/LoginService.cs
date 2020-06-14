using BankingSystem.Models;
using BankingSystem.Resources.Context;
using BankingSystem.Resources.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace BankingSystem.Services
{
    public class LoginService
    {
        public bool Login(string username, string password)
        {
            var result = false;

            try
            {
                using(var db = new BankingContext())
                {
                    var user = db.Users.FirstOrDefault(a => a.UserName == username);

                    if (user != null)
                    {
                        result = PasswordHash.ValidatePassword(password, user.PasswordHash );
                    }
                }
            }
            catch
            {
            }

            return result;
        }

        public bool Register(RegisterModel model)
        {
            var result = false;
            long bvnumber, acctnumber;
            try
            {
                using (var db = new BankingContext())
                {
                    var lastcustomer = db.Customers.AsNoTracking().OrderByDescending(a => a.Id).FirstOrDefault();
                    if (lastcustomer == null)
                    {
                        bvnumber = 124234531;
                        acctnumber = 001123156783;
                    }
                    else
                    {
                        bvnumber = lastcustomer.BVN + 1;
                        acctnumber = lastcustomer.AccountNumber + 1;
                    }

                    var customer = new Customer()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Phone = model.Phone,
                        Email = model.Email,
                        Balance = 0,
                        BVN = bvnumber,
                        AccountNumber = acctnumber,
                        DateCreated = DateTime.Now,
                        PinHash = PasswordHash.CreateHash(model.Pin),
                        User = new User()
                        {
                            PasswordHash = PasswordHash.CreateHash(model.FirstName),
                            UserName = model.Email,
                            RoleName = model.RoleName,
                            CreatedOn = DateTime.Now
                        }
                    };
                    db.Customers.Add(customer);
                    result = db.SaveChanges() > 0;
                }
            }
            catch
            {
                
            }

            return result;
        }

        public bool ResetPassword(string email, out string message)
        {
            var result = false;
            message = string.Empty;
            try
            {
                using (var context = new BankingContext())
                {
                    var user = context.Users.FirstOrDefault(a => a.UserName.ToLower() == email.ToLower());
                    if(user != null)
                    {
                        var bytes = Encoding.UTF8.GetBytes(email + "!" + DateTime.Now.ToString());
                        var code = Convert.ToBase64String(bytes);
                        var body = $@"<h3>Dear Customer, </h3><p>Please click on the link below for your password reset.</p><br/>
                                    <p><a href='https://localhost:44376/account/resetpassword?g={code}'>Reset</a></p>";

                        var passwordRecord = new RecoverPassword
                        {
                            ExpiryTime = DateTime.Now.AddMinutes(15),
                            RecoveryCode = code,
                            UserId = user.Id
                        };

                        EmailService.SendEmail(email, "Password Recovery", body, true);

                        context.RecoverPasswords.Add(passwordRecord);
                        context.SaveChanges();

                        message = $"Recovery mail successfully sent to {email}";
                    }
                    else
                    {
                        message = "User with supplied email address not found";
                    }
                }
            }
            catch(Exception ex)
            {
                message = ex.Message;
            }
            return result;
        }

        public bool IsCodeValid(string code, out string message)
        {
            var result = false;
            message = string.Empty;
            try
            {
                using(var context = new BankingContext())
                {
                    var pRecord = context.RecoverPasswords.FirstOrDefault(a => a.RecoveryCode == code);
                    if(pRecord == null)
                    {
                        message = "Invalid password reset link";
                    }
                    else
                    {
                        if(pRecord.ExpiryTime < DateTime.Now)
                        {
                            message = "Password expiry link has expired. Please do Forgot Password again";
                        }
                        else
                        {

                            message = "Success";
                            result = true;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                message = ex.Message;
            }
            return result;
        }

        public bool ConfirmResetPassword(ResetPasswordModel m)
        {
            var result = false;
            try
            {
                using(var db = new BankingContext())
                {
                    var user = db.Users.FirstOrDefault(a => a.UserName == m.Email);

                    user.PasswordHash = PasswordHash.CreateHash(m.NewPassword);

                    result = db.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
            return result;
        }
    }
}