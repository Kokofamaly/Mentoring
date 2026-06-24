using Xunit;
using Moq;
using ShopSystem.Infrastructure.Interfaces;
using ShopSystem.Services;
using ShopSystem.Models;
using ShopSystem.Enum;
using ShopSystem.Infrastructure;


namespace ShopSystem.Tests
{
    public class OrdersManagerTests
    {
        private readonly Mock<IOrdersRepository> _mockOrdersRepository;
        private readonly OrdersManager _ordersManager;

        public OrdersManagerTests()
        {
            _mockOrdersRepository = new Mock<IOrdersRepository>();
            _ordersManager = new OrdersManager(_mockOrdersRepository.Object);
        }

        [Fact]
        public void CreateOrder_WithValidOrder_CallsRepositoryAdd()
        {
            var order = new Order
            {
                Id = 1,
                ProductId = 100,
                Status = Enum.OrderStatus.NotStarted,
                CreatedDate = DateTimeOffset.Now,
                UpdatedDate = DateTimeOffset.Now
            };

            _ordersManager.CreateOrder(order);

            _mockOrdersRepository.Verify(repo => repo.Add(It.Is<Order>(o => o.Id == 1 && o.ProductId == 100)), Times.Once);
        }
        [Fact]
        public void CreateOrder_WithNullOrder_ThrowsArgumentNullException()
        {
            Order? order = null;

            Assert.Throws<ArgumentNullException>(() =>  _ordersManager.CreateOrder(order));
            _mockOrdersRepository.Verify(repo => repo.Add(It.IsAny<Order>()), Times.Never);

        }
        [Fact]
        public void UpdateOrder_WithValdiOrder_CallsRepositoryUpdate()
        {
            var order = new Order
            {
                Id = 1,
                ProductId = 100,
                Status = Enum.OrderStatus.InProgress,
                CreatedDate = DateTimeOffset.Now,
                UpdatedDate = DateTimeOffset.Now
            };

            _ordersManager.UpdateOrder(order);

            _mockOrdersRepository.Verify(repo => repo.Update(It.Is<Order>(o => o.Id == 1 && o.Status == OrderStatus.InProgress)), Times.Once);
        }
        [Fact]
        public void UpdateOrder_WithNullOrder_ThrowsArgumentNullException()
        {
            Order? order = null;

            Assert.Throws<ArgumentNullException>(() => _ordersManager.UpdateOrder(order));
            _mockOrdersRepository.Verify(repo => repo.Update(It.IsAny<Order>()), Times.Never);
        }
        [Fact]
        public void DeleteOrder_WithValidOrder_CallsRepositoryDelete()
        {
            var order = new Order
            {
                Id = 1,
                ProductId = 100,
                Status = OrderStatus.Cancelled,
            };

            _ordersManager.DeleteOrder(order);

            _mockOrdersRepository.Verify(repo => repo.Delete(It.Is<Order>(o => o.Id == 1)), Times.Once);
        }
        [Fact]
        public void DeleteOrder_WithNullOrder_ThrowsArgumentNullException()
        {
            Order? order = null;

            Assert.Throws<ArgumentNullException>(() => _ordersManager.DeleteOrder(order));
            _mockOrdersRepository.Verify(repo => repo.Delete(It.IsAny<Order>()), Times.Never);
        }
        [Fact]
        public void DeleteBulkOrder_WithValidFilter_CallsRepositoryDeleteBulk()
        {
            var filter = new OrderFilter { Year = 2024, Status = OrderStatus.Done };

            _ordersManager.DeleteBulkOrders(filter);

            _mockOrdersRepository.Verify(repo => repo.DeleteBulk(It.Is<OrderFilter>(f => f.Year == 2024 && f.Status == OrderStatus.Done)), Times.Once);
        }
        [Fact]
        public void GetOrder_WithValidId_ReturnsOrder()
        {
            var excpectedOrder = new Order
            {
                Id = 1,
                ProductId = 100,
                Status = OrderStatus.Arrived,
                CreatedDate = DateTimeOffset.Now,
                UpdatedDate = DateTimeOffset.Now
            };
            _mockOrdersRepository.Setup(repo => repo.GetById(1)).Returns(excpectedOrder);

            var result = _ordersManager.GetOrder(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal(100, result.ProductId);
            Assert.Equal(OrderStatus.Arrived, result.Status);
            _mockOrdersRepository.Verify(repo => repo.GetById(1), Times.Once);

        }
        [Fact]
        public void GetOrders_WithValidFilter_ReturnsOrders()
        {
            var filter = new OrderFilter { Year = 2024 };
            var expecterOrdes = new List<Order>
            {
                new Order {Id = 1, ProductId = 100, Status = OrderStatus.Done},
                new Order {Id = 2, ProductId = 101, Status = OrderStatus.Done}
            };
            _mockOrdersRepository.Setup(repo => repo.GetAll(filter)).Returns(expecterOrdes);

            var result = _ordersManager.GetOrders(filter);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            _mockOrdersRepository.Verify(repo => repo.GetAll(filter), Times.Once);

        }
        [Fact]
        public void GetOrders_WithNullFilter_ReturnsAllOrders()
        {
            var expectedOrders = new List<Order> 
            {
                new Order {Id = 1, ProductId = 100, Status = OrderStatus.NotStarted},
                new Order {Id = 2, ProductId = 101, Status = OrderStatus.InProgress}
            };
            _mockOrdersRepository.Setup(repo => repo.GetAll(null)).Returns(expectedOrders);

            var result = _ordersManager.GetOrders(null);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            _mockOrdersRepository.Verify(repo => repo.GetAll(null), Times.Once);
        }
    }
}
