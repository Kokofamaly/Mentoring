using ShopSystem.Infrastructure.Interfaces;
using ShopSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopSystem.Services
{
    public class ProductsManager
    {
        private readonly IProductsRepository _productsRepository;
        public ProductsManager(IProductsRepository repo)
        {
            _productsRepository = repo;
        }
        public void CreateProduct() { }
        public IEnumerable<Product> GetProducts() { }
        public Product GetProduct() { }
        public void DeleteProduct() { }
        public void UpdateProduct() { }
    }
}
