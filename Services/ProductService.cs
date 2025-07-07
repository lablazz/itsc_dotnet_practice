using itsc_dotnet_practice.Models;
using itsc_dotnet_practice.Models.ModelDtos.ProductDto;
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
            (await _repository.GetAllAsync()).OrderBy(p => p.Id);

        public async Task<Product?> GetProductByIdAsync(int id) =>
            await _repository.GetByIdAsync(id);

        public async Task<Product> CreateProductAsync(ProductModelRequest request)
        {
            // Check if Name is null or empty
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ArgumentException("Product name must not be empty.");
            }

            // Optional: Check if product with same name already exists
            var existingProduct = await _repository.GetByNameAsync(request.Name);
            if (existingProduct != null)
            {
                throw new InvalidOperationException("A product with the same name already exists.");
            }

            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Stock = request.Stock
            };

            await _repository.AddAsync(product);
            return product;
        }

        public async Task<Product?> UpdateProductAsync(int id, ProductModelRequest product)
        {
            return await _repository.UpdateAsync(new Product
            {
                Id = id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock
            });
        }

        public async Task<bool> DeleteProductAsync(int id) =>
            await _repository.DeleteAsync(id);
    }
}