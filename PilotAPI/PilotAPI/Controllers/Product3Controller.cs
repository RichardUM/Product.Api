using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PilotAPI.Data;
using PilotAPI.Model;
using PilotAPI.Services;


namespace PilotAPI.Controllers
{
    [ApiVersion("3.0")]
    [Route("api/Products")]
    [ApiController]
    public class Product3Controller : ControllerBase
    {
        
        IProducts productDbRepo;

        public Product3Controller(IProducts _productDbRepo)
        {
            productDbRepo = _productDbRepo;
        }
        // GET: api/Product3
        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            return productDbRepo.GetProducts();
        }

        // GET: api/Products2/5
        [HttpGet("{id}", Name = "Get3")]
        public IActionResult GetProduct(int id)
        {
            var product = productDbRepo.GetProduct(id);
            if (product == null)
            {
                return NotFound("NO records Found");
            }

            return Ok(product);
        }

        // POST: api/Products2
        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            if (!TryValidateModel(product))
            {
                return BadRequest(ModelState);
            }

            productDbRepo.AddProduct(product);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT: api/Products2/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product product)
        {
            if (!TryValidateModel(product))
            {
                return BadRequest(ModelState);
            }

            if (id != product.ProductId)
            {
                return BadRequest();
            }

            try
            {
                productDbRepo.UpdateProduct(product);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return NotFound("No Record found");
            }


            return Ok("Product updated correctly");
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            productDbRepo.DeleteProduct(id);

            return Ok("Product Deleted");


        }
    }
}
