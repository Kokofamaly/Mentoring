using ShopSystem.Models;

namespace ShopSystem.Infrastructure.Interfaces
{
    public interface IOrdersRepository
    {
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(Order order);
        Task DeleteBulkAsync(OrderFilter? filter);
        Task<Order?> GetByIdAsync(int id);
        Task<IEnumerable<Order>> GetAllAsync(OrderFilter? filter);
    }
}
