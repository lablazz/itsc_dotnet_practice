using DotNetEnv;
using itsc_dotnet_practice.Data;
using itsc_dotnet_practice.Models;
using itsc_dotnet_practice.Repositories.Interface;
using itsc_dotnet_practice.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace itsc_dotnet_practice.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    public UserRepository(AppDbContext context) => _context = context;

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

    public async Task CreateUserAsync(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }
}
