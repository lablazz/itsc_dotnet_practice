using itsc_dotnet_practice.Models;
using itsc_dotnet_practice.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace itsc_dotnet_practice.Repositories.Interface;

public interface IOrderRepository
{
    Task<List<Order>> GetAllOrdersAsync();
    Task<List<Order>> GetOrdersByStatusAsync(string status);
    Task<Order> CreateOrderAsync(OrderDto.Request request);
    Task<Order> UpdateOrderStatusAsync(int orderId, string newStatus);
    Task<List<Order>> GetOrdersByUserIdAsync(int userId);
}
