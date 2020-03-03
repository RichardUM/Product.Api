using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PilotAPI.Model;

namespace PilotAPI.Services
{
    public interface IProducts
    {
        //CRUD Methods

        IEnumerable<Product> GetProducts();
        Product GetProduct(int id);
        void AddProduct(Product prod);
        void UpdateProduct(Product prod);
        void DeleteProduct(int id);



    }
}
