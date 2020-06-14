using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BankingSystem.Resources.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string RoleName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

    }
}