using Microsoft.AspNetCore.Mvc;
using itsc_dotnet_practice.Services.Interface;
using System.Threading.Tasks;

namespace itsc_dotnet_practice.Controllers;

[ApiController]
[Route("api/admin")]
public class AdminController : ControllerBase
{
    private readonly IAuthService _authService;
    public AdminController(IAuthService authService) => _authService = authService;

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

    //[HttpPost("approve-order")]
    //public async Task<IActionResult> ApproveOrder([FromBody] Models.Dtos.OrderDto.OrderRequest orderRequest)
    //{
    //    var authHeader = Request.Headers["Authorization"].ToString();
    //    var user = _authService.ValidateBasicAuth(authHeader);
    //    if (user == null || user.Role != "Admin")
    //        return Unauthorized("Admin access only");
    //    var result = await _authService.ApproveOrder(orderRequest);
    //    if (result.IsSuccess)
    //        return Ok(result.Data);

    //    return BadRequest(result.ErrorMessage);
    //}
}
