using itsc_dotnet_practice.Models;

namespace itsc_dotnet_practice.Services.Interface;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(int id);
    Task<User> CreateUserAsync(User user);
    Task<bool> UpdateUserAsync(int id, User user);
    Task<bool> DeleteUserAsync(int id);
}
