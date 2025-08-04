using itsc_dotnet_practice.Models;
using itsc_dotnet_practice.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace itsc_dotnet_practice.Services.Interface;

// Services/Interface/IOrderService.cs
public interface IOrderService
{
    Task<List<Order>> GetAllOrders();
    Task<List<Order>> GetOrdersByStatus(string status);
    Task<List<Order>> GetOrdersByUserId(int userId);
    Task<List<Order>> GetOrdersByUserIdAndStatus(int userId, string status);
    Task<Order> CreateOrder(OrderDto.OrderRequest request);
    Task<Order> UpdateShippingAddress(int orderId, string newShippingAddress);
    Task<Order> CancelOrder(int orderId);
    Task<List<Order>> ApproveOrder(OrderApprovalDto orderRequest);
}

