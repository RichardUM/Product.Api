using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PilotAPI.Model;

namespace PilotAPI.Controllers
{
    [Route("api/Products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        static List<Product> _products = new List<Product>()
        {
            new Product() {ProductId = 0, ProductName = "Shirt", ProductPrice = "250"},
            new Product() {ProductId = 1, ProductName = "Shorts", ProductPrice = "300"},
            new Product() {ProductId = 3, ProductName = "Jacket", ProductPrice = "700"}

        };

        [HttpGet]
        public IActionResult GetProduct()
        {
            return Ok(_products);
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody]Product prod)
        {
            _products.Add(prod);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public void PutProduct(int id, [FromBody] Product prod)
        {
            _products[id] = prod;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _products.RemoveAt(id);
        }

    }
}