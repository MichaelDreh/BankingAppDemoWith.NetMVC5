using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BankingSystem.Resources.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [Required,StringLength(50)]
        public string FirstName { get; set; }

        [Required,StringLength(50)]
        public string LastName { get; set; }

        [Required,StringLength(50), Index(IsClustered = false, IsUnique = true)]
        public string Email { get; set; }

        [Required, StringLength(14, MinimumLength = 11)]
        public string Phone { get; set; }

        [Required]
        public Int64 AccountNumber { get; set; }

        [Required]
        public Int64 BVN { get; set; }

        public decimal Balance { get; set; }

        [Required]
        public string PinHash { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}