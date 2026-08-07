using Moq;
using ShopSystem.Infrastructure.Interfaces;
using ShopSystem.Models;
using ShopSystem.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ShopSystem.Tests
{
    public class ProductsManagerTests
    {
        private readonly Mock<IProductsRepository> _mockProductsRepository;
        private readonly ProductsManager _productsManager;

        public ProductsManagerTests() { 
            _mockProductsRepository = new Mock<IProductsRepository>();
            _productsManager = new ProductsManager(_mockProductsRepository.Object);
        }

        [Fact]
        public void CreateProduct_WithValidProduct_CallsRepositoryAdd()
        {
            var product = new Product 
            { 
              Id = 1,
              Name = "Test Product",
              Description = "A test product",
              Weight = 1.5m,
              Height = 10.0m,
              Width = 5.0m,
              Length = 3.0m
            };

            _productsManager.CreateProduct(product);

            _mockProductsRepository.Verify(repo => repo.Add(It.Is<Product>(p => p.Id == 1 && p.Name == "Test Product")), Times.Once);
        }
        [Fact]
        public void CreateProduct_WithNullProduct_ThrowsArgumentNullException()
        {
            Product? product = null;

            Assert.Throws<ArgumentNullException>(() => _productsManager.CreateProduct(product));
            _mockProductsRepository.Verify(repo => repo.Add(It.IsAny<Product>()), Times.Never);
        }
        [Fact]
        public void UpdateProduct_WithValidProduct_CallsRepositoryUpdate()
        {
            var product = new Product
            {
                Id = 1,
                Name = "Updated Product",
                Description = "Updated description",
                Weight = 2.0m,
                Height = 15.0m,
                Width = 8.0m,
                Length = 6.0m
            };

            _productsManager.UpdateProduct(product);

            _mockProductsRepository.Verify(repo => repo.Update(It.Is<Product>(p => p.Name == "Updated Product")), Times.Once);
        }
        [Fact]
        public void UpdateProduct_WithNullProduct_ThrowsArgumentNullException()
        {
            Product? product = null;

            Assert.Throws<ArgumentNullException>(() => _productsManager.UpdateProduct(product));
            _mockProductsRepository.Verify(repo => repo.Update(It.IsAny<Product>()), Times.Never);
        }
        [Fact]
        public void DeleteProduct_WithValidProduct_CallsRepositoryDelete()
        {
            var product = new Product
            {
                Id = 1,
                Name = "Product to Delete",
                Weight = 1.0m
            };

            _productsManager.DeleteProduct(product);

            _mockProductsRepository.Verify(repo => repo.Delete(It.Is<Product>(p => p.Id == 1)), Times.Once);
        }
        [Fact]
        public void DeleteProduct_WithNullProduct_ThrowsArgumentNullException()
        {
            Product? product = null;

            Assert.Throws<ArgumentNullException>(() => _productsManager.DeleteProduct(product));
            _mockProductsRepository.Verify(repo => repo.Delete(It.IsAny<Product>()), Times.Never);

        }
        [Fact]
        public void GetProduct_WithValidId_ReturnsProduct()
        {
            var expectedProduct = new Product
            {
                Id = 1,
                Name = "Laptop",
                Description = "High-perfomance laptop",
                Weight = 2.5m,
                Height = 1.0m,
                Width = 30.0m,
                Length = 20.0m
            };
            _mockProductsRepository.Setup(repo => repo.GetById(1)).Returns(expectedProduct);

            var result = _productsManager.GetProduct(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Laptop", result.Name);
            Assert.Equal(2.5m, result.Weight);
            _mockProductsRepository.Verify(repo => repo.GetById(1), Times.Once);
        }
        [Fact]
        public void GetProduct_WithNonExistentId_ThrowsKeyNotFoundException()
        {
            _mockProductsRepository.Setup(repo => repo.GetById(It.IsAny<int>())).Returns((Product?)null);
            
            Assert.Throws<KeyNotFoundException>(() => _productsManager.GetProduct(9999));
        }
        [Fact]
        public void GetProducts_ReturnsAllProduct()
        {
            var expectedProducts = new List<Product>
            {
                new Product {Id = 1, Name = "Product 1", Weight = 1.0m, Height = 10.0m, Width = 5.0m, Length = 3.0m},
                new Product {Id = 2, Name = "Product 2", Weight = 2.0m, Height = 20.0m, Width = 10.0m, Length = 6.0m},
                new Product {Id = 3, Name = "Product 3", Weight = 1.5m, Height = 15.0m, Width = 7.0m, Length = 4.0m}
            };
            _mockProductsRepository.Setup(repo => repo.GetAll()).Returns(expectedProducts);

            var result = _productsManager.GetProducts();

            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
            Assert.Contains(result, p => p.Name == "Product 1");
            Assert.Contains(result, p => p.Name == "Product 2");
            Assert.Contains(result, p => p.Name == "Product 3");
            _mockProductsRepository.Verify(repo => repo.GetAll(), Times.Once());

        }

    }
}
