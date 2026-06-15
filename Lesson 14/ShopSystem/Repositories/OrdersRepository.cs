using Microsoft.EntityFrameworkCore;
using ShopSystem.Data;
using ShopSystem.Infrastructure;
using ShopSystem.Infrastructure.Interfaces;
using ShopSystem.Models;

namespace ShopSystem.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly ShopDbContext _context;

        public OrdersRepository(ShopDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(Order order)
        {
            
        }

        public async Task UpdateAsync(Order order)
        {
            
        }

        public async Task DeleteAsync(Order order)
        {
            
        }

        public async Task DeleteBulkAsync(OrderFilter? filter)
        {
            
        }

        public async Task<Order?> GetByIdAsync(int id)
        {

        }

        public async Task<IEnumerable<Order>> GetAllAsync(OrderFilter? filter)
        {
            
        }
    }
}
