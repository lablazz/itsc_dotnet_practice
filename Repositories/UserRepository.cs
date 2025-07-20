using AutoMapper;
using DotNetEnv;
using itsc_dotnet_practice.Data;
using itsc_dotnet_practice.Models;
using itsc_dotnet_practice.Models.Dtos;
using itsc_dotnet_practice.Repositories.Interface;
using itsc_dotnet_practice.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace itsc_dotnet_practice.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public UserRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<User?> GetUserAsync(string username, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

        if (user == null || !EncryptionUtility.VerifyPassword(password, user.Password))
        {
            return null;
        }

        return user;
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<User> CreateUserAsync(RegisterRequestDto user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        var existingUser = await GetUserByUsernameAsync(user.Username);
        if (existingUser != null)
        {
            throw new Exception("Username already exists");
        }
        if (user.Password != user.ConfirmPassword)
        {
            throw new Exception("Passwords do not match");
        }
        var newUser = _mapper.Map<User>(user);
        newUser.Password = EncryptionUtility.HashPassword(user.Password);

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();
        return newUser;
    }
}