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
        public void CreateProduct(Product? product) {

            ArgumentNullException.ThrowIfNull(product);
            _productsRepository.Add(product);

        }
        public IEnumerable<Product> GetProducts() { 

            return _productsRepository.GetAll();

        }
        public Product GetProduct(int id) {
            
            var product = _productsRepository.GetById(id);
            ArgumentNullException.ThrowIfNull(product);
            return product;

        }
        public void DeleteProduct(Product? product) {

            ArgumentNullException.ThrowIfNull(product);
            _productsRepository.Delete(product);

        }
        public void UpdateProduct(Product? product) {
            ArgumentNullException.ThrowIfNull(product);
            _productsRepository.Update(product);
        }
    }
}
