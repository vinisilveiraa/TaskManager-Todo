using Microsoft.EntityFrameworkCore;
using ToDoApi.Data;
using ToDoApi.Models;

namespace ToDoApi.Repositories;

public class ItemRepository
{
    private readonly AppDbContext _context;
    public ItemRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<Item>> GetAllAsync()
    {
        return await _context.Items
            .ToListAsync();
    }
    public async Task<List<Item>> GetByUserIdAsync(int userId, string? status, string? sort)
    {
        var query = _context.Items
            .Where(q => q.UserId == userId);

        if (status == "completed") query = query.Where(q => q.IsCompleted);
        if (status == "pending") query = query.Where(q => !q.IsCompleted);

        query = sort switch
        {
            "oldest" => query.OrderBy(q => q.Created_At),
            "newest" => query.OrderByDescending(q => q.Created_At),
            _ => query.OrderByDescending(q => q.Created_At),
        };
        
        return await query.ToListAsync();
    }
    public async Task<Item?> GetByIdAsync(int id)
    {
        return await _context.Items
            .FindAsync(id);
    }
    public async Task AddAsync(Item todoItem)
    {
        _context.Items.Add(todoItem);

        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(int id, Item todoItem)
    {
        var existingTodo = await GetByIdAsync(id);
        if (existingTodo == null)
            return;

        existingTodo.Title = todoItem.Title;
        existingTodo.Description = todoItem.Description;

        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var todoItem = await GetByIdAsync(id);
        if (todoItem == null)
            return;

        _context.Items.Remove(todoItem);

        await _context.SaveChangesAsync();
    }
    public async Task PatchStatusAsync(int id)
    {
        var todoItem = await GetByIdAsync(id);
        if (todoItem == null)
            return;

        todoItem.IsCompleted = !todoItem.IsCompleted;

        if (todoItem.IsCompleted)
        {
            todoItem.Completed_At = DateTime.UtcNow;
        }
        else
        {
            todoItem.Completed_At = null;
        }
        await _context.SaveChangesAsync();
    }
    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Items
            .AnyAsync(t => t.Id == id);
    }
}