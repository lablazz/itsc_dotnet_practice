using Microsoft.AspNetCore.Mvc;
using itsc_dotnet_practice.Services.Interface;
using System.Threading.Tasks;
using itsc_dotnet_practice.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace itsc_dotnet_practice.Controllers;

[ApiController]
[Route("api/admin")]
public class AdminController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IOrderService _orderService;
    public AdminController(IAuthService authService, IOrderService orderService)
    {
        _authService = authService;
        _orderService = orderService;
    }

    [HttpGet("login")]
    public IActionResult Dashboard()
    {
        var authHeader = Request.Headers["Authorization"].ToString();
        var user = _authService.ValidateBasicAuth(authHeader);
        if (user == null || user.Role != "Admin")
            return Unauthorized("Admin access only");
        var token = _authService.GenerateJwtToken(user);

        return Ok(new { token });
    }

    [HttpPost("approve-order")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ApproveOrder([FromBody] OrderApprovalDto orderRequest)
    {
        if (orderRequest == null || orderRequest.OrderIds.Count == 0)
            return BadRequest("Order request cannot be null or empty");
        var result = await _orderService.ApproveOrder(orderRequest);
        if (result == null || result.Count == 0)
            return NotFound("No orders found to approve");
        return Ok("Order approved successfully");
    }
}
