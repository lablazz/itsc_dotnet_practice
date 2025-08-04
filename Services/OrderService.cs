using itsc_dotnet_practice.Models;
using itsc_dotnet_practice.Models.Dtos;
using itsc_dotnet_practice.Repositories.Interface;
using itsc_dotnet_practice.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace itsc_dotnet_practice.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repo;
    public OrderService(IOrderRepository repo)
    {
        _repo = repo;
    }
    public async Task<List<Order>> GetAllOrders()
    {
        return await _repo.GetAllOrders();
    }
    public async Task<List<Order>> GetOrdersByStatus(string status)
    {
        return await _repo.GetOrdersByStatus(status);
    }
    public async Task<List<Order>> GetOrdersByUserId(int userId)
    {
        return await _repo.GetOrdersByUserId(userId);
    }
    public async Task<List<Order>> GetOrdersByUserIdAndStatus(int userId, string status)
    {
        var orders = await _repo.GetOrdersByUserId(userId);
        if (status == "All")
        {
            return orders;
        }
        return orders.FindAll(o => o.Status.Equals(status, StringComparison.OrdinalIgnoreCase));
    }
    public async Task<Order> CreateOrder(OrderDto.OrderRequest request)
    {
        return await _repo.CreateOrder(request);
    }
    public async Task<Order> UpdateShippingAddress(int orderId, string newShippingAddress)
    {
        return await _repo.UpdateShippingAddress(orderId, newShippingAddress);
    }
    public async Task<Order> CancelOrder(int orderId)
    {
        return await _repo.CancelOrder(orderId);
    }

    public async Task<List<Order>> ApproveOrder(OrderApprovalDto orderRequest)
    {
        if (orderRequest == null || orderRequest.OrderIds.Count == 0)
        {
            throw new ArgumentNullException(nameof(orderRequest), "Order request cannot be null or empty");
        }
        var orders = new List<Order>();
        foreach (var orderId in orderRequest.OrderIds)
        {
            var order = await _repo.GetOrderById(orderId);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {orderId} not found");
            }
            if (order.Status != "Pending")
            {
                throw new InvalidOperationException($"Order with ID {orderId} is not in a pending state");
            }
            orders.Add(await _repo.UpdateOrderStatus(order.Id, "Approved"));
        }
        return orders;
    }
}
