using itsc_dotnet_practice.Models;
using System.Threading.Tasks;

namespace itsc_dotnet_practice.Repositories.Interface;

public interface IUserRepository
{
    Task<User?> GetUserAsync(string username, string password);
    Task<User?> GetUserByUsernameAsync(string username);
    Task CreateUserAsync(User user);
}
