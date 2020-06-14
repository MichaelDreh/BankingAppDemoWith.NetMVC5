using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BankingSystem.Resources.Entities
{
    public class RecoverPassword
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [Required,StringLength(100)]
        public string RecoveryCode { get; set; }

        public DateTime ExpiryTime { get; set; }


        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}