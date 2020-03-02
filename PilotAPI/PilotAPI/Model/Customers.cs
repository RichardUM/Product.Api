using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PilotAPI.Model
{
    public class Customers

    {
        public int CustId { get; set; }
        [Required,StringLength(15)]
        public string CustName { get; set; }
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Invalid email format ")]
        public string CustEmail { get; set; }

        public string CustNo { get; set; }

    }
}
