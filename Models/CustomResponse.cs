using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingSystem.Models
{
    public class CustomResponse
    {
        public bool Successful { get; set; }
        public string Message { get; set; }
    }
}