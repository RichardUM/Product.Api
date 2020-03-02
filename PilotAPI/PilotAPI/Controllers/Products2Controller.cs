using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PilotAPI.Data;
using PilotAPI.Model;

namespace PilotAPI.Controllers
{
    [Route("api/Products2")]
    [ApiController]
    public class Products2Controller : ControllerBase
    {

        ProductDbContext productDbContext;

        public Products2Controller(ProductDbContext _productDbContext)
        {
            productDbContext = _productDbContext;
        }

        // GET: api/Products2
        [HttpGet]
        public IEnumerable<Product> Get(String sortPrice)
        {
            IQueryable<Product> products;
            switch (sortPrice)
            {
                case "desc":
                    products = productDbContext.Products.OrderByDescending(p => p.ProductPrice);
                    break;
                case "asc":
                    products = productDbContext.Products.OrderBy(p => p.ProductPrice);
                    break;
                default:
                    products = productDbContext.Products;
                    break;
            }
            return products;
        }

        [HttpGet]
        [Route("search")]
        public IEnumerable<Product> GetSearch(String search)
        {
            var prod = productDbContext.Products.Where(p => p.ProductName.StartsWith(search));
            return prod;
        }

        [HttpGet]
        [Route("pages")]
        public IEnumerable<Product> Get(int? pageNo,int? pageSize)
        {
            var product = from p in productDbContext.Products.OrderBy(a => a.ProductId) select p;

            int currentPage = pageNo ?? 1;
            int currentPageSize = pageSize ?? 5;

            var items = product.Skip((currentPage - 1) * currentPageSize).Take(currentPageSize).ToList();
            return items;
        }

        // GET: api/Products2/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult  Get(int id)
        {
            var product = productDbContext.Products.SingleOrDefault(m => m.ProductId == id);
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

            productDbContext.Products.Add(product);
            productDbContext.SaveChanges(true);
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
                productDbContext.Products.Update(product);
                productDbContext.SaveChanges(true);
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
            var product = productDbContext.Products.SingleOrDefault(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound("No Record Found....");
            }

            productDbContext.Products.Remove(product);
            productDbContext.SaveChanges(true);
            return Ok("Product Deleted");


        }
    }
}
