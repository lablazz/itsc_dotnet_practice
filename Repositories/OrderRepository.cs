using AutoMapper;
using itsc_dotnet_practice.Data;
using itsc_dotnet_practice.Models;
using itsc_dotnet_practice.Models.Dtos;
using itsc_dotnet_practice.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace itsc_dotnet_practice.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;
    // automapper
    private readonly IMapper _mapper;
    public OrderRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
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
    // to get all orders for status = ["Pending", "confirmed", "Processing", "Completed", "Cancelled"]
    public async Task<List<Order>> GetOrdersByStatusAsync(string status)
    {
        if (string.IsNullOrEmpty(status))
        {
            throw new ArgumentException("Status cannot be null or empty.", nameof(status));
        }
        var validStatuses = new[] { "pending", "confirmed", "processing", "completed", "cancelled" };
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
    public async Task<Order> CreateOrderAsync(OrderDto.OrderRequest request)
    {
        var order = _mapper.Map<Order>(request);
        if (order == null)
        {
            throw new ArgumentNullException(nameof(order), "Order cannot be null.");
        }
        _context.Orders.Add(order);
        _context.SaveChanges();
        return order;
    }

    // to change status of order from "Pending" to "confirmed", "Processing", "Completed", or "Cancelled"
    public async Task<Order> UpdateOrderStatusAsync(int orderId, string newStatus)
    {
        if (string.IsNullOrEmpty(newStatus))
        {
            throw new ArgumentException("Status cannot be null or empty.", nameof(newStatus));
        }
        var validStatuses = new[] { "pending", "confirmed", "processing", "completed", "cancelled" };
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
        _context.SaveChanges();
        return order;
    }
    // to update ShippingAddress in order
    public async Task<Order> UpdateShippingAddressAsync(int orderId, string newShippingAddress)
    {
        if (string.IsNullOrEmpty(newShippingAddress))
        {
            throw new ArgumentException("Shipping address cannot be null or empty.", nameof(newShippingAddress));
        }
        var order = await _context.Orders.FindAsync(orderId);
        if (order == null)
        {
            throw new KeyNotFoundException($"Order with ID {orderId} not found.");
        }
        this.UpdateOrderStatusAsync(orderId, "confirmed").Wait();
        order.ShippingAddress = newShippingAddress;
        order.UpdatedAt = DateTime.UtcNow;
        _context.Orders.Update(order);
        _context.SaveChanges();
        return order;
    }
    // update status to "Cancelled" for user
    public async Task<Order> CancelOrderAsync(int orderId)
    {
        var order = await _context.Orders.FindAsync(orderId);
        if (order == null)
        {
            throw new KeyNotFoundException($"Order with ID {orderId} not found.");
        }
        if (order.Status == "Cancelled")
        {
            throw new InvalidOperationException($"Order with ID {orderId} is already cancelled.");
        }
        order.Status = "Cancelled";
        order.UpdatedAt = DateTime.UtcNow;
        _context.Orders.Update(order);
        _context.SaveChanges();
        return order;
    }
}
