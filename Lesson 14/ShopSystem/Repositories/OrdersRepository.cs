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
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Order order)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBulkAsync(OrderFilter? filter)
        {
            var query = _context.Orders.AsQueryable();

                        if(filter != null)
            {
                if(filter.Year.HasValue) query = query.Where(o => o.CreatedDate.Year == filter.Year.Value);
                if(filter.Month.HasValue) query = query.Where(o => o.CreatedDate.Month == filter.Month.Value);
                if(filter.ProductId.HasValue) query = query.Where(o => o.ProductId == filter.ProductId.Value);
                if(filter.Status.HasValue) query = query.Where(o => o.Status == filter.Status.Value);
            }

            var ordersToDelete = await query.ToListAsync();
            _context.Orders.RemoveRange(ordersToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Order>> GetAllAsync(OrderFilter? filter)
        {
            var query = _context.Orders.AsQueryable();

            if(filter != null)
            {
                if(filter.Year.HasValue) query = query.Where(o => o.CreatedDate.Year == filter.Year.Value);
                if(filter.Month.HasValue) query = query.Where(o => o.CreatedDate.Month == filter.Month.Value);
                if(filter.ProductId.HasValue) query = query.Where(o => o.ProductId == filter.ProductId.Value);
                if(filter.Status.HasValue) query = query.Where(o => o.Status == filter.Status.Value);
            }

            return await query.ToListAsync();
        }
    }
}
