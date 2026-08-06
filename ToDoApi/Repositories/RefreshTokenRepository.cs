using Microsoft.EntityFrameworkCore;
using ToDoApi.Data;
using ToDoApi.Models;

namespace ToDoApi.Repositories;

public class RefreshTokenRepository
{
    private readonly AppDbContext _context;
    public RefreshTokenRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<RefreshToken>> GetAllAsync()
    {
        return await _context.RefreshTokens
            .ToListAsync();
    }
    public async Task<RefreshToken?> GetTokenAsync(string token)
    {
        return await _context.RefreshTokens
            .Where(x => x.Token == token)
            .FirstOrDefaultAsync();
    }
    public async Task<RefreshToken?> GetByUserIdAsync(int userId)
    {
        return await _context.RefreshTokens
            .Where(x => x.UserId == userId)
            .FirstOrDefaultAsync();
    }
    public async Task AddAsync(RefreshToken refreshToken)
    {
        _context.RefreshTokens.Add(refreshToken);

        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(string token)
    {
        var refreshToken = await GetTokenAsync(token);
        if (refreshToken == null)
            return;

        _context.RefreshTokens.Remove(refreshToken);

        await _context.SaveChangesAsync();
    }
    public async Task DeleteByUserIdAsync(int id)
    {
        await _context.RefreshTokens.Where(x => x.UserId == id).ExecuteDeleteAsync();
    }

    public async Task DeleteExpiredAsync()
    {
        await _context.RefreshTokens.Where(x => x.Expiry < DateTime.UtcNow).ExecuteDeleteAsync();
    }
}