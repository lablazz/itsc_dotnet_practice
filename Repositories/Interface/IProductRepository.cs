using itsc_dotnet_practice.Models;
using itsc_dotnet_practice.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace itsc_dotnet_practice.Repositories.Interface;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(int id);
    Task<List<Product>> GetProductByQuery(string query);
    Task<Product> CreateProductAsync(ProductDto.Request product);
    Task<Product> UpdateProductAsync(int id, Product product);
    Task<bool> DeleteProductAsync(int id);
}
