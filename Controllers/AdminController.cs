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

    [HttpGet("dashboard")]
    public async Task<IActionResult> Dashboard()
    {
        var authHeader = Request.Headers["Authorization"].ToString();
        var user = await _authService.ValidateBasicAuthAsync(authHeader);
        if (user == null || user.Role != "Admin")
            return Unauthorized("Admin access only");

        return Ok("Welcome Admin!");
    }
}
