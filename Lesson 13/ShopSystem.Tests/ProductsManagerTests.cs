using Moq;
using ShopSystem.Infrastructure.Interfaces;
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

        }
        [Fact]
        public void CreateProduct_WithNullProduct_ThrowsArgumentNullException()
        {

        }
        [Fact]
        public void UpdateProduct_WithValidProduct_CallsRepositoryUpdate()
        {

        }
        [Fact]
        public void UpdateProduct_WithNullProduct_ThrowsArgumentNullException()
        {

        }
        [Fact]
        public void DeleteProduct_WithValidProduct_CallsRepositoryDelete()
        {

        }
        [Fact]
        public void DeleteProduct_WithNullProduct_ThrowsArgumentNullException()
        {

        }
        [Fact]
        public void GetProduct_WithValidId_ReturnsProduct()
        {

        }
        [Fact]
        public void GetProduct_WithNonExistentId_ThrowsKeyNotFoundException()
        {

        }
        [Fact]
        public void GetProducts_ReturnsAllProduct()
        {

        }

    }
}
