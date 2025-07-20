using itsc_dotnet_practice.Models;
using itsc_dotnet_practice.Models.Dtos;
using itsc_dotnet_practice.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace itsc_dotnet_practice.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService _service;

    public OrderController(IOrderService service)
    {
        _service = service;
    }

    // Admin only: Get all orders
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<Order>>> GetAllOrders()
    {
        var orders = await _service.GetAllOrdersAsync();
        return Ok(orders);
    }

    // Admin only: Get orders by status
    [HttpGet("status/{status}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<Order>>> GetOrdersByStatus(string status)
    {
        var orders = await _service.GetOrdersByStatusAsync(status);
        return Ok(orders);
    }

    // Admin only: Get orders by userId
    [HttpGet("user/{userId}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<Order>>> GetOrdersByUserId(int userId)
    {
        var orders = await _service.GetOrdersByUserIdAsync(userId);
        return Ok(orders);
    }

    // Admin only: Get orders by userId and status
    [HttpGet("user/{userId}/status/{status}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<Order>>> GetOrdersByUserIdAndStatus(int userId, string status)
    {
        var orders = await _service.GetOrdersByUserIdAndStatusAsync(userId, status);
        return Ok(orders);
    }

    // Admin only: Create new order
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Order>> CreateOrder([FromBody] OrderDto.OrderRequest request)
    {
        var newOrder = await _service.CreateOrderAsync(request);
        return CreatedAtAction(nameof(GetAllOrders), new { id = newOrder.Id }, newOrder);
    }

    // User can cancel own order by orderId (update status to "Cancel")
    [HttpPut("{orderId}/cancel")]
    [Authorize]
    public async Task<ActionResult<Order>> CancelOrder(int orderId)
    {
        var cancelledOrder = await _service.CancelOrderAsync(orderId);

        if (cancelledOrder == null)
            return NotFound($"Order with ID {orderId} not found or can't be cancelled.");

        return Ok(cancelledOrder);
    }
}
