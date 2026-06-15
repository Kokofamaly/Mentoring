using ShopSystem.Infrastructure;
using ShopSystem.Infrastructure.Interfaces;
using ShopSystem.Models;

namespace ShopSystem.Services
{
    public class OrdersManager
    {
        private readonly IOrdersRepository _ordersRepository;

        public OrdersManager(IOrdersRepository repo)
        {
            _ordersRepository = repo ?? throw new ArgumentNullException();
        }

        public async Task CreateOrderAsync(Order? order)
        {

        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(OrderFilter? filter)
        {
        }

        public async Task<Order> GetOrderAsync(int id)
        {

        }

        public async Task DeleteOrderAsync(Order? order)
        {

        }

        public async Task DeleteBulkOrdersAsync(OrderFilter? filter)
        {
        }

        public async Task UpdateOrderAsync(Order? order)
        {

        }
    }
}
