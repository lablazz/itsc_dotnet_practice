using itsc_dotnet_practice.Models;

namespace itsc_dotnet_practice.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order?> GetByIdAsync(int id);
        Task<Order> CreateAsync(Order order);
        Task<bool> DeleteAsync(int id);
    }
}
