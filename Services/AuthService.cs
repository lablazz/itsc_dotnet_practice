using AutoMapper;
using DotNetEnv;
using Humanizer;
using itsc_dotnet_practice.Models;
using itsc_dotnet_practice.Models.Dtos;
using itsc_dotnet_practice.Repositories.Interface;
using itsc_dotnet_practice.Services.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace itsc_dotnet_practice.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepo;
    private readonly IConfiguration _config;

    public AuthService(IUserRepository userRepo, IConfiguration config)
    {
        _userRepo = userRepo;
        _config = config;
    }

    public async Task<User?> AuthenticateAsync(LoginRequestDto login)
    {
        return await _userRepo.GetUserAsync(login.Username, login.Password);
    }

    public User? ValidateBasicAuth(string authHeader)
    {
        if (string.IsNullOrWhiteSpace(authHeader)) return null;

        var encoded = authHeader.Replace("Basic ", "");
        var decoded = Encoding.UTF8.GetString(Convert.FromBase64String(encoded));
        var parts = decoded.Split(':');
        if (parts.Length != 2) return null;

        //return await _userRepo.GetValidationBasicAuth(parts[0], parts[1]);
        if (Env.GetString("ADMIN_USERNAME") == null || Env.GetString("ADMIN_PASSWORD") == null)
        {
            throw new System.Exception("ADMIN_USERNAME or ADMIN_PASSWORD is not set.");
        }
        if (Env.GetString("ADMIN_USERNAME") != parts[0] || Env.GetString("ADMIN_PASSWORD") != parts[1])
        {
            return null;
        }
        return new User
        {
            Username = parts[0],
            Role = "Admin"
        };
    }

    public string GenerateJwtToken(User user)
    {
        var claims = new[] {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT_KEY"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["JWT_ISSUER"],
            audience: _config["JWT_AUDIENCE"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(5),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<User> RegisterAsync(RegisterRequestDto register)
    {
        if (register == null) throw new ArgumentNullException(nameof(register));
        var existingUser = await _userRepo.GetUserByUsernameAsync(register.Username);
        if (existingUser != null)
        {
            throw new Exception("Username already exists");
        }
        if (register.Password != register.ConfirmPassword)
        {
            throw new Exception("Passwords do not match");
        }
        return await _userRepo.CreateUserAsync(register);
    }
}