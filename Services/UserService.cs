using itsc_dotnet_practice.Models;
using itsc_dotnet_practice.Models.ModelDtos.UserDto;
using itsc_dotnet_practice.Repositories.Interface;
using itsc_dotnet_practice.Services.Interface;
using itsc_dotnet_practice.Utilities;
using Microsoft.EntityFrameworkCore;

namespace itsc_dotnet_practice.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CreateUserDtoResponse>> GetAllUsersAsync()
    {
        var users = await _repository.GetAllAsync();

        return users.Select(user => new CreateUserDtoResponse
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Phone = EncryptionUtility.IsBase64(user.Phone)
                ? EncryptionUtility.DecryptString(user.Phone)
                : user.Phone // fallback if not encrypted
        }).OrderBy(u => u.Id);
    }

    public async Task<CreateUserDtoResponse?> GetUserByIdAsync(int id)
    {
        var user = await _repository.GetByIdAsync(id);
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
        // Validate passwords match
        if (dto.Password != dto.ConfirmPassword)
            throw new ArgumentException("Passwords do not match.");

        // Check for existing email
        bool emailExists = await _repository.GetByEmailAsync(dto.Email);
        if (emailExists)
            throw new ArgumentException("A user with this email already exists.");

        // Create User entity with encrypted values
        var user = new User
        {
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Password = EncryptionUtility.HashPassword(dto.Password), // Use hash for passwords
            Phone = EncryptionUtility.EncryptString(dto.Phone)       // Symmetric for phone
        };

        // Save to repository
        var createdUser = await _repository.CreateAsync(user);

        // Return response DTO with decrypted phone
        return new CreateUserDtoResponse
        {
            Id = createdUser.Id,
            Email = createdUser.Email,
            FirstName = createdUser.FirstName,
            LastName = createdUser.LastName,
            Phone = EncryptionUtility.DecryptString(createdUser.Phone)
        };
    }

    public async Task<bool> UpdateUserAsync(int id, UserUpdateDtoRequest userDto)
    {
        var existingUser = await _repository.GetByIdAsync(id);
        if (existingUser == null) return false;

        if (!EncryptionUtility.CompareEncryptedString(existingUser.Password, userDto.Password))
        {
            throw new ArgumentException("Invalid Email or Password, Please try again");
        }

        // ✅ Manual field-by-field mapping from DTO to entity
        existingUser.Email = userDto.Email;
        existingUser.FirstName = userDto.FirstName;
        existingUser.LastName = userDto.LastName;
        existingUser.Phone = EncryptionUtility.EncryptString(userDto.Phone);
        existingUser.Password = EncryptionUtility.HashPassword(userDto.Password);

        return await _repository.UpdateAsync(existingUser);
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}