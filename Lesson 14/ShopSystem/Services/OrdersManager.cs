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
            ArgumentNullException.ThrowIfNull(order);
            await _ordersRepository.AddAsync(order);
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(OrderFilter? filter)
        {
            return await _ordersRepository.GetAllAsync(filter);
        }

        public async Task<Order> GetOrderAsync(int id)
        {
            var order = await _ordersRepository.GetByIdAsync(id);
            if(order == null) throw new KeyNotFoundException();
            return order;
        }

        public async Task DeleteOrderAsync(Order? order)
        {
            ArgumentNullException.ThrowIfNull(order);
            await _ordersRepository.DeleteAsync(order);
        }

        public async Task DeleteBulkOrdersAsync(OrderFilter? filter)
        {
            await _ordersRepository.DeleteBulkAsync(filter);
        }

        public async Task UpdateOrderAsync(Order? order)
        {
            ArgumentNullException.ThrowIfNull(order);
            await _ordersRepository.UpdateAsync(order);
        }
    }
}
