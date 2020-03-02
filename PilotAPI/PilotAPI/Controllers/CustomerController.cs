using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PilotAPI.Model;

namespace PilotAPI.Controllers
{
    [Route("api/Customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private static List<Customers> _customer = new List<Customers>()
        {
            new Customers(){CustId = 0,CustName = "James",CustEmail = "shs1@d.com",CustNo = "0889766552"},
            new Customers(){CustId = 1,CustName = "Jame",CustEmail = "shs2@d.com",CustNo = "0789766552"},
            new Customers(){CustId = 2,CustName = "Jam",CustEmail = "shs3@d.com",CustNo = "0689766552"}
        };

        public IEnumerable<Customers> Get()
        {
            return _customer;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Customers cust)
        {
            if (ModelState.IsValid)
            {
                _customer.Add(cust);
                return Ok();
            }

            return BadRequest(ModelState);
        }
    }
}