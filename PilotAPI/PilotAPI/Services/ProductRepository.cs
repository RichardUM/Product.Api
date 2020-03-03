using PilotAPI.Data;
using PilotAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PilotAPI.Services
{
    public class ProductRepository : IProducts
    {
        ProductDbContext productDbContext;

        public ProductRepository(ProductDbContext _productDbContext)
        {
            productDbContext = _productDbContext;
        }
        public IEnumerable<Product> GetProducts()
        {
            return productDbContext.Products;
        }

        public Product GetProduct(int id)
        {
            var product = productDbContext.Products.SingleOrDefault(m => m.ProductId == id);
            return product;
        }

        public void AddProduct(Product prod)
        {
            productDbContext.Products.Add(prod);
            productDbContext.SaveChanges(true);
        }

        public void UpdateProduct(Product prod)
        {
            productDbContext.Products.Update(prod);
            productDbContext.SaveChanges(true);
        }

        public void DeleteProduct(int id)
        {
            var product = productDbContext.Products.Find( id);
            productDbContext.Products.Remove(product);
            productDbContext.SaveChanges(true);
        }
    }
}
