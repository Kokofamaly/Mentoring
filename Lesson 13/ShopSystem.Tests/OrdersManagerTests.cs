using Xunit;
using Moq;
using ShopSystem.Infrastructure.Interfaces;
using ShopSystem.Services;
using ShopSystem.Models;


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

        }
        [Fact]
        public void UpdateOrder_WithValdiOrder_CallsRepositoryUpdate()
        {
                
        }
        [Fact]
        public void UpdateOrder_WithNullOrder_ThrowsArgumentNullException()
        {

        }
        [Fact]
        public void DeleteOrder_WithValidOrder_CallsRepositoryDelete()
        {

        }
        [Fact]
        public void DeleteOrder_WithNullOrder_ThrowsArgumentNullException()
        {

        }
        [Fact]
        public void DeleteBulkOrder_WithValidFilter_CallsRepositoryDeleteBulk()
        {

        }
        [Fact]
        public void GetOrder_WithVAlidId_ReturnsOrder()
        {

        }
        [Fact]
        public void GetOrders_WithValidFilter_ReturnsOrders()
        {
                
        }
        [Fact]
        public void GetOrders_WithNullFilter_ReturnsAllOrders()
        {

        }
    }
}
