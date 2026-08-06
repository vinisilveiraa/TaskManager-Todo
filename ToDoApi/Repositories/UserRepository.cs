using Microsoft.EntityFrameworkCore;
using ToDoApi.Data;
using ToDoApi.Models;

namespace ToDoApi.Repositories;

public class UserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users
            .ToListAsync();
    }
    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users
            .FindAsync(id);
    }
    public async Task<User?> GetByUserNameAsync(string userName)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.UserName == userName);
    }
    public async Task AddAsync(User user)
    {
        _context.Users.Add(user);

        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(int id, User user)
    {
        var existingUser = await GetByIdAsync(id);
        if (existingUser == null)
            return;

        existingUser.UserName = user.UserName;

        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var user = await GetByIdAsync(id);

        if (user == null)
            return;

        _context.Users.Remove(user);

        await _context.SaveChangesAsync();
    }
    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Users
            .AnyAsync(u => u.Id == id);
    }
    public async Task PatchRoleAsync(int id)
    {
        var user = await GetByIdAsync(id);
        if (user == null)
            return;

        if (user.Role == UserRole.User)
            user.Role = UserRole.Admin;
        else
            user.Role = UserRole.User;

        await _context.SaveChangesAsync();
    }
}