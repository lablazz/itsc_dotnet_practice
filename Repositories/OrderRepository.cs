using itsc_dotnet_practice.Data;
using itsc_dotnet_practice.Models;
using itsc_dotnet_practice.Models.Dtos;
using itsc_dotnet_practice.Repositories.Interface;
using itsc_dotnet_practice.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace itsc_dotnet_practice.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;
    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }
    // to get all ordrers
     public async Task<List<Order>> GetAllOrdersAsync()
    {
        return await _context.Orders
            .Include(o => o.User)
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
            .ToListAsync();
    }
    // to get all orders for status = ["Pending", "Approved", "Processing", "Completed", "Cancelled"]
    public async Task<List<Order>> GetOrdersByStatusAsync(string status)
    {
        if (string.IsNullOrEmpty(status))
        {
            throw new ArgumentException("Status cannot be null or empty.", nameof(status));
        }
        var validStatuses = new[] { "pending", "approved", "processing", "completed", "cancelled" };
        if (!validStatuses.Contains(status.ToLower()))
        {
            throw new ArgumentException($"Invalid status. Valid statuses are: {string.Join(", ", validStatuses)}", nameof(status));
        }
        // Fetch orders with the specified status
        // and include related User and OrderDetails with Products
        if (status == "All")
        {
            return await GetAllOrdersAsync();
        }
        return await _context.Orders
            .Where(o => o.Status == status)
            .Include(o => o.User)
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
            .ToListAsync();
    }
    // to get all orders for spacific user
    public async Task<List<Order>> GetOrdersByUserIdAsync(int userId)
    {
        return await _context.Orders
            .Where(o => o.UserId == userId)
            .Include(o => o.User)
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
            .ToListAsync();
    }
    public async Task<Order> CreateOrderAsync(OrderDto.Request request)
    {
        var order = await MapperUtility.MapToOrderAsync(request, _context);
        if (order == null)
        {
            throw new ArgumentNullException(nameof(order), "Order cannot be null.");
        }
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }
    // to change status of order from "Pending" to "Approved", "Processing", "Completed", or "Cancelled"
    public async Task<Order> UpdateOrderStatusAsync(int orderId, string newStatus)
    {
        if (string.IsNullOrEmpty(newStatus))
        {
            throw new ArgumentException("Status cannot be null or empty.", nameof(newStatus));
        }
        var validStatuses = new[] { "pending", "approved", "processing", "completed", "cancelled" };
        if (!validStatuses.Contains(newStatus.ToLower()))
        {
            throw new ArgumentException($"Invalid status. Valid statuses are: {string.Join(", ", validStatuses)}", nameof(newStatus));
        }
        var order = await _context.Orders.FindAsync(orderId);
        if (order == null)
        {
            throw new KeyNotFoundException($"Order with ID {orderId} not found.");
        }
        order.Status = newStatus;
        order.UpdatedAt = DateTime.UtcNow;
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
        return order;
    }
}
