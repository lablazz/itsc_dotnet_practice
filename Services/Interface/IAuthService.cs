using itsc_dotnet_practice.Models;
using itsc_dotnet_practice.Models.Dtos;
using System.Threading.Tasks;

namespace itsc_dotnet_practice.Services.Interface;

public interface IAuthService
{
    Task<User?> AuthenticateAsync(LoginRequestDto login);
    Task<User?> ValidateBasicAuthAsync(string authHeader);
    string GenerateJwtToken(User user);
    Task<bool> RegisterAsync(RegisterRequestDto register);

}
