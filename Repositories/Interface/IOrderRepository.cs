using itsc_dotnet_practice.Models;
using itsc_dotnet_practice.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace itsc_dotnet_practice.Repositories.Interface;

// Repositories/Interface/IOrderRepository.cs
public interface IOrderRepository
{
    Task<List<Order>> GetAllOrdersAsync();
    Task<List<Order>> GetOrdersByStatusAsync(string status);
    Task<List<Order>> GetOrdersByUserIdAsync(int userId);
    Task<Order> CreateOrderAsync(OrderDto.OrderRequest request);
    Task<Order> UpdateOrderStatusAsync(int orderId, string newStatus);
    Task<Order> UpdateShippingAddressAsync(int orderId, string newShippingAddress);
    Task<Order> CancelOrderAsync(int orderId);
}

