using Microsoft.EntityFrameworkCore;
using ShopSystem.Data;
using ShopSystem.Infrastructure.Interfaces;
using ShopSystem.Models;
using ShopSystem.Repositories;
using ShopSystem.Services;
using Xunit;

namespace ShopSystem.Tests
{
    public class ProductsManagerTests
    {
        private ShopDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<ShopDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new ShopDbContext(options);
        }

        [Fact]
        public async Task CreateProductAsync_WithValidProduct_ShouldAddProduct()
        {
            using var context = CreateInMemoryContext();
            var repository = new ProductsRepository(context);
            var manager = new ProductsManager(repository);
            var product = new Product 
            { 
                Name = "Test Product",
                Description = "Test Description",
                Weight = 10m,
                Height = 20m,
                Width = 30m,
                Length = 40m
            };

            await manager.CreateProductAsync(product);

            var result = await context.Products.FirstOrDefaultAsync(p => p.Name == "Test Product");
            Assert.NotNull(result);
            Assert.Equal("Test Description", result.Description);
        }

        [Fact]
        public async Task CreateProductAsync_WithNullProduct_ShouldThrowException()
        {
            using var context = CreateInMemoryContext();
            var repository = new ProductsRepository(context);
            var manager = new ProductsManager(repository);

            await Assert.ThrowsAsync<ArgumentNullException>(() => manager.CreateProductAsync(null));
        }

        [Theory]
        [InlineData("Product1", "Desc1")]
        [InlineData("Product2", "Desc2")]
        [InlineData("Product3", null)]
        public async Task CreateProductAsync_WithVariousProducts_ShouldSucceed(string name, string? description)
        {
            using var context = CreateInMemoryContext();
            var repository = new ProductsRepository(context);
            var manager = new ProductsManager(repository);
            var product = new Product 
            { 
                Name = name,
                Description = description,
                Weight = 5m,
                Height = 10m,
                Width = 15m,
                Length = 20m
            };

            await manager.CreateProductAsync(product);

            var result = await context.Products.FirstOrDefaultAsync(p => p.Name == name);
            Assert.NotNull(result);
            Assert.Equal(description, result.Description);
        }

        [Fact]
        public async Task GetProductAsync_WithValidId_ShouldReturnProduct()
        {
            using var context = CreateInMemoryContext();
            var product = new Product 
            { 
                Id = 1,
                Name = "Test Product",
                Description = "Test",
                Weight = 10m,
                Height = 20m,
                Width = 30m,
                Length = 40m
            };
            context.Products.Add(product);
            await context.SaveChangesAsync();

            var repository = new ProductsRepository(context);
            var manager = new ProductsManager(repository);

            var result = await manager.GetProductAsync(1);

            Assert.NotNull(result);
            Assert.Equal("Test Product", result.Name);
        }

        [Fact]
        public async Task GetProductAsync_WithInvalidId_ShouldThrowKeyNotFoundException()
        {
            using var context = CreateInMemoryContext();
            var repository = new ProductsRepository(context);
            var manager = new ProductsManager(repository);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => manager.GetProductAsync(999));
        }

        [Fact]
        public async Task GetProductsAsync_ShouldReturnAllProducts()
        {
            using var context = CreateInMemoryContext();
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product1", Weight = 5m, Height = 10m, Width = 15m, Length = 20m },
                new Product { Id = 2, Name = "Product2", Weight = 5m, Height = 10m, Width = 15m, Length = 20m },
                new Product { Id = 3, Name = "Product3", Weight = 5m, Height = 10m, Width = 15m, Length = 20m }
            };
            context.Products.AddRange(products);
            await context.SaveChangesAsync();

            var repository = new ProductsRepository(context);
            var manager = new ProductsManager(repository);

            var result = await manager.GetProductsAsync();

            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async Task DeleteProductAsync_WithValidProduct_ShouldRemoveProduct()
        {
            using var context = CreateInMemoryContext();
            var product = new Product 
            { 
                Id = 1,
                Name = "Test Product",
                Weight = 10m,
                Height = 20m,
                Width = 30m,
                Length = 40m
            };
            context.Products.Add(product);
            await context.SaveChangesAsync();

            var repository = new ProductsRepository(context);
            var manager = new ProductsManager(repository);

            await manager.DeleteProductAsync(product);

            var result = await context.Products.FirstOrDefaultAsync(p => p.Id == 1);
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteProductAsync_WithNullProduct_ShouldThrowException()
        {
            using var context = CreateInMemoryContext();
            var repository = new ProductsRepository(context);
            var manager = new ProductsManager(repository);

            await Assert.ThrowsAsync<ArgumentNullException>(() => manager.DeleteProductAsync(null));
        }

        [Fact]
        public async Task UpdateProductAsync_WithValidProduct_ShouldUpdateProduct()
        {
            using var context = CreateInMemoryContext();
            var product = new Product 
            { 
                Id = 1,
                Name = "Original Name",
                Weight = 10m,
                Height = 20m,
                Width = 30m,
                Length = 40m
            };
            context.Products.Add(product);
            await context.SaveChangesAsync();

            product.Name = "Updated Name";

            var repository = new ProductsRepository(context);
            var manager = new ProductsManager(repository);

            await manager.UpdateProductAsync(product);

            var result = await context.Products.FirstOrDefaultAsync(p => p.Id == 1);
            Assert.NotNull(result);
            Assert.Equal("Updated Name", result.Name);
        }
    }
}
