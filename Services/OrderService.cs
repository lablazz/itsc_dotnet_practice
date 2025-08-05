﻿using itsc_dotnet_practice.Models;
using itsc_dotnet_practice.Models.Dtos;
using itsc_dotnet_practice.Repositories.Interface;
using itsc_dotnet_practice.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
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
    public async Task<Order> CreateOrder(OrderDto.OrderRequest request, int userId)
    {
        return await _repo.CreateOrder(request, userId);
    }

    public async Task<Order> UpdateOrderStatus(int orderId, string status)
    {
        var validStatuses = new[] { "pending", "confirm", "reject", "cancel" };

        var normalizedStatus = status.Trim();

        if (!validStatuses.Contains(normalizedStatus, StringComparer.OrdinalIgnoreCase))
        {
            throw new ArgumentException($"Invalid status. Valid statuses are: {string.Join(", ", validStatuses)}", nameof(status));
        }

        return await _repo.UpdateOrderStatus(orderId, normalizedStatus);
    }

}
