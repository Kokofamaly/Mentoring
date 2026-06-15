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

        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
        }

        public async Task<Product> GetProductAsync(int id)
        {

        }

        public async Task DeleteProductAsync(Product? product)
        {

        }

        public async Task UpdateProductAsync(Product? product)
        {

        }
    }
}
