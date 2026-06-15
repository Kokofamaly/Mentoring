using ShopSystem.Models;

namespace ShopSystem.Infrastructure.Interfaces
{
    public interface IProductsRepository
    {
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Product product);
        Task<Product?> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllAsync();
    }
}
