using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Models;

namespace OnlineStore.Services
{
    internal class ProductService
    {
        private List<Product> products;
        public ProductService()
        {
            products = new List<Product>();
        }
        public void AddProduct(Product product)
        {
            products.Add(product);
        }
        public void RemoveProduct(Product product)
        {
            products.Remove(product);
        }
        public Product GetProductById(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
                throw new ProductNotFoundException();

            return product;
        }
        public List<Product> ViewAllProducts()
        {
            return products;

        }
    }
}
