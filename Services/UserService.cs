using itsc_dotnet_practice.Models;
using itsc_dotnet_practice.Repositories.Interface;
using itsc_dotnet_practice.Services.Interface;

namespace itsc_dotnet_practice.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task<User> CreateUserAsync(User user)
    {
        return await _userRepository.CreateAsync(user);
    }

    public async Task<bool> UpdateUserAsync(int id, User user)
    {
        if (id != user.Id) return false;
        return await _userRepository.UpdateAsync(user);
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        return await _userRepository.DeleteAsync(id);
    }
}
