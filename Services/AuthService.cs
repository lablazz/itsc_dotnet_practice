using itsc_dotnet_practice.Models;
using itsc_dotnet_practice.Repositories.Interface;
using itsc_dotnet_practice.Services.Interface;
using itsc_dotnet_practice.Models.ModelDtos.AuthDto;

namespace itsc_dotnet_practice.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _repository;

    public AuthService(IAuthRepository repository)
    {
        _repository = repository;
    }

    //public async Task<LoginResponse?> LoginAsync(LoginRequest request)
    //{
    //    var user = await GetUserByUsernameAsync(u => u.Email == request.Username);

    //    if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
    //    {
    //        return null;
    //    }

    //    var tokenHandler = new JwtSecurityTokenHandler();
    //    var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);

    //    var tokenDescriptor = new SecurityTokenDescriptor
    //    {
    //        Subject = new ClaimsIdentity(new[]
    //        {
    //            new Claim(ClaimTypes.Name, user.Username),
    //            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
    //        }),
    //        Expires = DateTime.UtcNow.AddHours(2),
    //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
    //        Issuer = _config["Jwt:Issuer"],
    //        Audience = _config["Jwt:Audience"]
    //    };

    //    var token = tokenHandler.CreateToken(tokenDescriptor);
    //    return new LoginResponse { Token = tokenHandler.WriteToken(token) };
    //}
}
