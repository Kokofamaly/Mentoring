using ShopSystem.Infrastructure.Interfaces;
using ShopSystem.Models;

namespace ShopSystem.Services
{
    public class ProductsManager
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsManager(IProductsRepository repo)
        {
            _productsRepository = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public async Task CreateProductAsync(Product? product)
        {
            ArgumentNullException.ThrowIfNull(product);
            await _productsRepository.AddAsync(product);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _productsRepository.GetAllAsync();
        }

        public async Task<Product> GetProductAsync(int id)
        {
            var prod = await _productsRepository.GetByIdAsync(id);
            if(prod == null) throw new KeyNotFoundException();
            return prod;
        }

        public async Task DeleteProductAsync(Product? product)
        {
            ArgumentNullException.ThrowIfNull(product);
            await _productsRepository.DeleteAsync(product);
        }

        public async Task UpdateProductAsync(Product? product)
        {
            ArgumentNullException.ThrowIfNull(product);
            await _productsRepository.UpdateAsync(product);
        }
    }
}
