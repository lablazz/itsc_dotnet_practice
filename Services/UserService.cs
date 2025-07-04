using itsc_dotnet_practice.Models;
using itsc_dotnet_practice.Models.ModelDtos.UserDto;
using itsc_dotnet_practice.Repositories.Interface;
using itsc_dotnet_practice.Services.Interface;
using itsc_dotnet_practice.Utilities;

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

    public async Task<CreateUserDtoResponse> CreateUserAsync(CreateUserDtoRequest dto)
    {
        if (dto.Password != dto.ConfirmPassword)
            throw new ArgumentException("Passwords do not match.");

        bool emailExists = await _userRepository.GetByEmailAsync(dto.Email);
        if (emailExists)
            throw new ArgumentException("A user with this email already exists.");

        var user = new User
        {
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Password = EncryptionUtility.EncryptString(dto.Password),
            Phone = EncryptionUtility.EncryptString(dto.Phone)
        };

        var createdUser = await _userRepository.CreateAsync(user);

        return new CreateUserDtoResponse
        {
            Id = createdUser.Id,
            Email = createdUser.Email,
            FirstName = createdUser.FirstName,
            LastName = createdUser.LastName,
            Phone = EncryptionUtility.DecryptString(createdUser.Phone)
        };
    }

    public async Task<bool> UpdateUserAsync(int id, UserUpdateDtoRequest user)
    {
        if (id != user.Id) return false;
        return await _userRepository.UpdateAsync(user);
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        return await _userRepository.DeleteAsync(id);
    }

}
