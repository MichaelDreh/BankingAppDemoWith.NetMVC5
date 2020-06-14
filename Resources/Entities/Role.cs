using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankingSystem.Resources.Entities
{
    public class Role
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}