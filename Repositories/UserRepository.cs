using itsc_dotnet_practice.Data;
using itsc_dotnet_practice.Models;
using itsc_dotnet_practice.Models.ModelDtos.UserDto;
using itsc_dotnet_practice.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace itsc_dotnet_practice.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User> CreateAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> UpdateAsync(UserUpdateDtoRequest user)
    {
        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Users.AnyAsync(e => e.Id == user.Id))
                return false;
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> GetByEmailAsync(string email)
    {
        var emails = await _context.Users.FindAsync(email);
        return emails != null;
    }
}
