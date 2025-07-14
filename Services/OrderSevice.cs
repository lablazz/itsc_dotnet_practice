using itsc_dotnet_practice.Models;
using itsc_dotnet_practice.Models.Dtos;
using itsc_dotnet_practice.Repositories.Interface;
using itsc_dotnet_practice.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace itsc_dotnet_practice.Services;

public class OrderSevice : IOrderService
{
    private readonly IOrderRepository _repo;
    public OrderSevice(IOrderRepository repo)
    {
        _repo = repo;
    }
    public async Task<List<Order>> GetAllOrdersAsync()
    {
        return await _repo.GetAllOrdersAsync();
    }
    public async Task<List<Order>> GetOrdersByStatusAsync(string status)
    {
        return await _repo.GetOrdersByStatusAsync(status);
    }
    public async Task<Order> CreateOrderAsync(OrderDto.Request request)
    {
        return await _repo.CreateOrderAsync(request);
    }
    public async Task<Order> UpdateOrderStatusAsync(int orderId, string newStatus)
    {
        return await _repo.UpdateOrderStatusAsync(orderId, newStatus);
    }
    public async Task<List<Order>> GetOrdersByUserIdAsync(int userId)
    {
        return await _repo.GetOrdersByUserIdAsync(userId);
    }
    public async Task<List<Order>> GetOrdersByUserIdAndStatusAsync(int userId, string status)
    {
        var orders = await _repo.GetOrdersByUserIdAsync(userId);
        if (status == "All")
        {
            return orders;
        }
        return orders.FindAll(o => o.Status.Equals(status, StringComparison.OrdinalIgnoreCase));
    }
}
