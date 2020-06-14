using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankingSystem.Models
{
    public class ResetPasswordModel
    {
        public string Email { get; set; }

        public string  NewPassword { get; set; }

        [Compare("NewPassword")]
        public string  ConfirmPassword { get; set; }
    }
}