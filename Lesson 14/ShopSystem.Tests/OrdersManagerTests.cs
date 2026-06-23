using Microsoft.EntityFrameworkCore;
using ShopSystem.Data;
using ShopSystem.Enum;
using ShopSystem.Infrastructure;
using ShopSystem.Infrastructure.Interfaces;
using ShopSystem.Models;
using ShopSystem.Repositories;
using ShopSystem.Services;
using Xunit;

namespace ShopSystem.Tests
{
    public class OrdersManagerTests
    {
        private ShopDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<ShopDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new ShopDbContext(options);
        }

        [Fact]
        public async Task CreateOrderAsync_WithValidOrder_ShouldAddOrder()
        {
            using var context = CreateInMemoryContext();
            var repository = new OrdersRepository(context);
            var manager = new OrdersManager(repository);
            var order = new Order 
            { 
                ProductId = 1, 
                Status = OrderStatus.NotStarted,
                CreatedDate = DateTimeOffset.Now,
                UpdatedDate = DateTimeOffset.Now
            };

            await manager.CreateOrderAsync(order);

            var result = await context.Orders.FirstOrDefaultAsync(o => o.ProductId == 1);
            Assert.NotNull(result);
            Assert.Equal(OrderStatus.NotStarted, result.Status);
        }

        [Fact]
        public async Task CreateOrderAsync_WithNullOrder_ShouldThrowException()
        {
            using var context = CreateInMemoryContext();
            var repository = new OrdersRepository(context);
            var manager = new OrdersManager(repository);

            await Assert.ThrowsAsync<ArgumentNullException>(() => manager.CreateOrderAsync(null));
        }

        [Fact]
        public async Task GetOrderAsync_WithValidId_ShouldReturnOrder()
        {
            using var context = CreateInMemoryContext();
            var order = new Order 
            { 
                Id = 1,
                ProductId = 1, 
                Status = OrderStatus.Done,
                CreatedDate = DateTimeOffset.Now,
                UpdatedDate = DateTimeOffset.Now
            };
            context.Orders.Add(order);
            await context.SaveChangesAsync();

            var repository = new OrdersRepository(context);
            var manager = new OrdersManager(repository);

            var result = await manager.GetOrderAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal(OrderStatus.Done, result.Status);
        }

        [Fact]
        public async Task GetOrderAsync_WithInvalidId_ShouldThrowKeyNotFoundException()
        {
            using var context = CreateInMemoryContext();
            var repository = new OrdersRepository(context);
            var manager = new OrdersManager(repository);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => manager.GetOrderAsync(999));
        }

        [Fact]
        public async Task DeleteOrderAsync_WithValidOrder_ShouldRemoveOrder()
        {
            using var context = CreateInMemoryContext();
            var order = new Order 
            { 
                Id = 1,
                ProductId = 1, 
                Status = OrderStatus.NotStarted,
                CreatedDate = DateTimeOffset.Now,
                UpdatedDate = DateTimeOffset.Now
            };
            context.Orders.Add(order);
            await context.SaveChangesAsync();

            var repository = new OrdersRepository(context);
            var manager = new OrdersManager(repository);

            await manager.DeleteOrderAsync(order);

            var result = await context.Orders.FirstOrDefaultAsync(o => o.Id == 1);
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteOrderAsync_WithNullOrder_ShouldThrowException()
        {
            using var context = CreateInMemoryContext();
            var repository = new OrdersRepository(context);
            var manager = new OrdersManager(repository);

            await Assert.ThrowsAsync<ArgumentNullException>(() => manager.DeleteOrderAsync(null));
        }

        [Fact]
        public async Task UpdateOrderAsync_WithValidOrder_ShouldUpdateOrder()
        {
            using var context = CreateInMemoryContext();
            var order = new Order 
            { 
                Id = 1,
                ProductId = 1, 
                Status = OrderStatus.NotStarted,
                CreatedDate = DateTimeOffset.Now,
                UpdatedDate = DateTimeOffset.Now
            };
            context.Orders.Add(order);
            await context.SaveChangesAsync();

            order.Status = OrderStatus.Done;

            var repository = new OrdersRepository(context);
            var manager = new OrdersManager(repository);

            await manager.UpdateOrderAsync(order);

            var result = await context.Orders.FirstOrDefaultAsync(o => o.Id == 1);
            Assert.NotNull(result);
            Assert.Equal(OrderStatus.Done, result.Status);
        }

        [Fact]
        public async Task GetOrdersAsync_WithValidFilter_ShouldReturnFilteredOrders()
        {
            using var context = CreateInMemoryContext();
            var orders = new List<Order>
            {
                new Order { Id = 1, ProductId = 1, Status = OrderStatus.Done, CreatedDate = new DateTimeOffset(2024, 6, 1, 0, 0, 0, TimeSpan.Zero), UpdatedDate = DateTimeOffset.Now },
                new Order { Id = 2, ProductId = 2, Status = OrderStatus.Done, CreatedDate = new DateTimeOffset(2024, 6, 15, 0, 0, 0, TimeSpan.Zero), UpdatedDate = DateTimeOffset.Now },
                new Order { Id = 3, ProductId = 1, Status = OrderStatus.NotStarted, CreatedDate = new DateTimeOffset(2024, 6, 20, 0, 0, 0, TimeSpan.Zero), UpdatedDate = DateTimeOffset.Now }
            };
            context.Orders.AddRange(orders);
            await context.SaveChangesAsync();

            var repository = new OrdersRepository(context);
            var manager = new OrdersManager(repository);
            var filter = new OrderFilter { Status = OrderStatus.Done };

            var result = await manager.GetOrdersAsync(filter);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task DeleteBulkOrdersAsync_WithFilter_ShouldDeleteMultipleOrders()
        {
            using var context = CreateInMemoryContext();
            var orders = new List<Order>
            {
                new Order { Id = 1, ProductId = 1, Status = OrderStatus.Done, CreatedDate = new DateTimeOffset(2024, 6, 1, 0, 0, 0, TimeSpan.Zero), UpdatedDate = DateTimeOffset.Now },
                new Order { Id = 2, ProductId = 2, Status = OrderStatus.Done, CreatedDate = new DateTimeOffset(2024, 6, 15, 0, 0, 0, TimeSpan.Zero), UpdatedDate = DateTimeOffset.Now },
                new Order { Id = 3, ProductId = 1, Status = OrderStatus.NotStarted, CreatedDate = new DateTimeOffset(2024, 6, 20, 0, 0, 0, TimeSpan.Zero), UpdatedDate = DateTimeOffset.Now }
            };
            context.Orders.AddRange(orders);
            await context.SaveChangesAsync();

            var repository = new OrdersRepository(context);
            var manager = new OrdersManager(repository);
            var filter = new OrderFilter { Status = OrderStatus.Done };

            await manager.DeleteBulkOrdersAsync(filter);

            var remaining = await context.Orders.CountAsync();
            Assert.Equal(1, remaining);
        }
    }
}
