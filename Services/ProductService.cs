using itsc_dotnet_practice.Models;
using itsc_dotnet_practice.Repositories.Interface;
using itsc_dotnet_practice.Services.Interface;

namespace itsc_dotnet_practice.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync() =>
            await _repository.GetAllAsync();

        public async Task<Product?> GetProductByIdAsync(int id) =>
            await _repository.GetByIdAsync(id);

        public async Task CreateProductAsync(Product product) =>
            await _repository.AddAsync(product);

        public async Task<Product?> UpdateProductAsync(Product product) =>
            await _repository.UpdateAsync(product);

        public async Task<bool> DeleteProductAsync(int id) =>
            await _repository.DeleteAsync(id);
    }
}