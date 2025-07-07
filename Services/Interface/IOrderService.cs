using itsc_dotnet_practice.Models;

namespace itsc_dotnet_practice.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order?> GetOrderByIdAsync(int id);
        Task<Order> CreateOrderAsync(Order order);
        Task<bool> DeleteOrderAsync(int id);
    }
}
