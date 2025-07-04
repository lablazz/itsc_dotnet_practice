using itsc_dotnet_practice.Models;
using itsc_dotnet_practice.Models.ModelDtos.UserDto;
using itsc_dotnet_practice.Repositories.Interface;
using itsc_dotnet_practice.Services.Interface;
using itsc_dotnet_practice.Utilities;
using Microsoft.EntityFrameworkCore;

namespace itsc_dotnet_practice.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<CreateUserDtoResponse>> GetAllUsersAsync()
    {
        return (IEnumerable<CreateUserDtoResponse>)await _userRepository.GetAllAsync();
    }

    public async Task<CreateUserDtoResponse?> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return null;

        return new CreateUserDtoResponse
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Phone = EncryptionUtility.DecryptString(user.Phone)
        };
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
        var existingUser = await _userRepository.GetByIdAsync(id);
        if (existingUser == null) return false;
        if (!EncryptionUtility.CompareEncryptedString(existingUser.Password, user.Password))
        {
            throw new ArgumentException("Invalid Email or Password, Please try again");
        }

        user.Id = id;
        return await _userRepository.UpdateAsync(user);
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        return await _userRepository.DeleteAsync(id);
    }

}
