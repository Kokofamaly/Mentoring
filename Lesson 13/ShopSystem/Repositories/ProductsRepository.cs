using ShopSystem.Infrastructure.Interfaces;
using ShopSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopSystem.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly string _connectionString;

        public ProductsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Product product)
        {

        }
        public void Add(IEnumerable<Product> products)
        {

        }
        public void Update(Product product)
        {

        }
        public void Delete(Product product)
        {

        }
        public Product? GetById(int id)
        {

        }
        public IEnumerable<Product> GetAll()
        {

        }
    }
}
