using itsc_dotnet_practice.Models;
using itsc_dotnet_practice.Models.Dtos;
using System.Threading.Tasks;

namespace itsc_dotnet_practice.Repositories.Interface;

public interface IUserRepository
{
    Task<User?> GetUserAsync(string username, string password);
    Task<User?> GetUserByUsernameAsync(string username);
    Task<User> CreateUserAsync(RegisterRequestDto user);
}
