﻿using itsc_dotnet_practice.Models;
using itsc_dotnet_practice.Models.ModelDtos.UserDto;

namespace itsc_dotnet_practice.Repositories.Interface;

public interface IUserRepository
{
    Task<IEnumerable<CreateUserDtoResponse>> GetAllAsync();

    Task<User?> GetByIdAsync(int id);

    Task<User> CreateAsync(User user);

    Task<bool> UpdateAsync(User user);

    Task<bool> DeleteAsync(int id);

    Task<bool> GetByEmailAsync(string email);
}