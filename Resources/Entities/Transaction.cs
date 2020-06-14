using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankingSystem.Resources.Entities
{
    public class Transaction
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public string TransactionId { get; set; }

        public string ReceiverName { get; set; }
        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string  TransType { get; set; }

        public double Charges { get; set; }

        [Required]
        public string  Channel { get; set; }

        public DateTime TransDate { get; set; } = DateTime.Now;

        
        public virtual Customer Customer { get; set; }
    }
}