using ShopSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopSystem.Infrastructure.Interfaces
{
    public interface IProductsRepository
    {
        public void Add(Product product);
        public void Add(IEnumerable<Product> products);
        public void Update(Product product);
        public void Delete(Product product);
        public Product? GetById(int id);
        public IEnumerable<Product> GetAll();
    }
}
