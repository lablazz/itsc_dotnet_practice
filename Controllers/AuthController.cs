using Microsoft.AspNetCore.Mvc;
using itsc_dotnet_practice.Models.ModelDtos.AuthDto;
using itsc_dotnet_practice.Services.Interface;

namespace itsc_dotnet_practice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    //[HttpPost("login")]
    //public async Task<IActionResult> Login([FromBody] LoginRequest request)
    //{
    //    var result = await _authService.LoginAsync(request);
    //    if (result == null)
    //        return Unauthorized("Invalid credentials");

    //    return Ok(result);
    //}
}
