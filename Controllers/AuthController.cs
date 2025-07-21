using Microsoft.AspNetCore.Mvc;
using itsc_dotnet_practice.Models.Dtos;
using itsc_dotnet_practice.Services.Interface;
using System.Threading.Tasks;

namespace itsc_dotnet_practice.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService) => _authService = authService;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto login)
    {
        var user = await _authService.AuthenticateAsync(login);
        if (user == null) return Unauthorized("Invalid credentials");
        var token = _authService.GenerateJwtToken(user);
        return Ok(new { token });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto register)
    {
        var result = await _authService.RegisterAsync(register);
        if (result == null) return BadRequest("Username already exists");

        return Ok("User registered successfully");
    }

}