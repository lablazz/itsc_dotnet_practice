using itsc_dotnet_practice.Models;
using itsc_dotnet_practice.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace itsc_dotnet_practice.Services.Interface;

// Services/Interface/IOrderService.cs
public interface IOrderService
{
    Task<List<Order>> GetAllOrdersAsync();
    Task<List<Order>> GetOrdersByStatusAsync(string status);
    Task<List<Order>> GetOrdersByUserIdAsync(int userId);
    Task<List<Order>> GetOrdersByUserIdAndStatusAsync(int userId, string status);
    Task<Order> CreateOrderAsync(OrderDto.OrderRequest request);
    Task<Order> UpdateShippingAddressAsync(int orderId, string newShippingAddress);
    Task<Order> CancelOrderAsync(int orderId);
}

