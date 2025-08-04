using AutoMapper;
using itsc_dotnet_practice.Models;
using itsc_dotnet_practice.Models.Dtos;
using itsc_dotnet_practice.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace itsc_dotnet_practice.Controllers;

[Route("api/orders")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService _service;
    private readonly IMapper _mapper;

    public OrderController(IOrderService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    // Admin only: Get all orders
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<OrderDto.OrderResponse>>> GetAllOrders()
    {
        var orders = await _service.GetAllOrders();
        var orderResponses = _mapper.Map<List<OrderDto.OrderResponse>>(orders);
        return Ok(orderResponses);
    }

    // Admin only: Get orders by status
    [HttpGet("status/{status}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<OrderDto.OrderResponse>>> GetOrdersByStatus(string status)
    {
        var orders = await _service.GetOrdersByStatus(status);
        var orderResponses = _mapper.Map<List<OrderDto.OrderResponse>>(orders);
        return Ok(orderResponses);
    }

    // Admin only: Get orders by userId
    [HttpGet("user/{userId}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<OrderDto.OrderResponse>>> GetOrdersByUserId(int userId)
    {
        var orders = await _service.GetOrdersByUserId(userId);
        var orderResponses = _mapper.Map<List<OrderDto.OrderResponse>>(orders);
        return Ok(orderResponses);
    }

    // Admin only: Get orders by userId and status
    [HttpGet("user/{userId}/status/{status}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<OrderDto.OrderResponse>>> GetOrdersByUserIdAndStatus(int userId, string status)
    {
        var orders = await _service.GetOrdersByUserIdAndStatus(userId, status);
        var orderResponses = _mapper.Map<List<OrderDto.OrderResponse>>(orders);
        return Ok(orderResponses);
    }

    // Create new order
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<OrderDto.OrderResponse>> CreateOrder([FromBody] OrderDto.OrderRequest request)
    {
        var newOrder = await _service.CreateOrder(request);
        var orderResponse = _mapper.Map<OrderDto.OrderResponse>(newOrder);
        return CreatedAtAction(nameof(GetAllOrders), new { id = orderResponse.Id }, orderResponse);
    }

    // User can update order details
    [HttpPut("shipping/{orderId}")]
    [Authorize]
    public async Task<ActionResult<OrderDto.OrderResponse>> UpdateShippingAddress(int orderId, [FromBody] string newShippingAddress)
    {
        var updatedOrder = await _service.UpdateShippingAddress(orderId, newShippingAddress);
        if (updatedOrder == null)
            return NotFound($"Order with ID {orderId} not found.");
        
        var orderResponse = _mapper.Map<OrderDto.OrderResponse>(updatedOrder);
        return Ok(orderResponse);
    }

    // User can cancel own order by orderId (update status to "Cancel")
    [HttpDelete("{orderId}/cancel")]
    [Authorize]
    public async Task<ActionResult<OrderDto.OrderResponse>> CancelOrder(int orderId)
    {
        var cancelledOrder = await _service.CancelOrder(orderId);

        if (cancelledOrder == null)
            return NotFound($"Order with ID {orderId} not found or can't be cancelled.");

        var orderResponse = _mapper.Map<OrderDto.OrderResponse>(cancelledOrder);
        return Ok(orderResponse);
    }
}
