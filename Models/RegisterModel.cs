using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BankingSystem.Models
{
    public class RegisterModel
    {
        [Required, StringLength(50)]
        public string FirstName { get; set; }

        [Required, StringLength(50)]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, StringLength(14, MinimumLength = 11)]
        public string Phone { get; set; }

        [Required, StringLength(4, MinimumLength = 4)]
        public string Pin { get; set; }

        [Required]
        public string RoleName { get; set; }

    }
}