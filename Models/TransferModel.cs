using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankingSystem.Models
{
    public class TransferModel
    {
        public int Id { get; set; }

        [Required]
        public Int64 AcountNumber { get; set; }

        public decimal Amount { get; set; }

    }
}