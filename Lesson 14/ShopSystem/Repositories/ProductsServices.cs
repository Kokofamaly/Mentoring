using Microsoft.EntityFrameworkCore;
using ShopSystem.Data;
using ShopSystem.Infrastructure.Interfaces;
using ShopSystem.Models;

namespace ShopSystem.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly ShopDbContext _context;

        public ProductsRepository(ShopDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(Product product)
        {

        }

        public async Task UpdateAsync(Product product)
        {

        }

        public async Task DeleteAsync(Product product)
        {

        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            
        }
    }
}
