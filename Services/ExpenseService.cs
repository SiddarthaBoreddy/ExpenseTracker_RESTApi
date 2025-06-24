using ExpenseTrackerApi.Data;
using ExpenseTrackerApi.DTOs;
using ExpenseTrackerApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace ExpenseTrackerApi.Services;

public class ExpenseService : IExpenseService
{
    private readonly AppDbContext _context;
    private readonly IMemoryCache _cache;

    public ExpenseService(AppDbContext context, IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
    }

    public async Task<IEnumerable<Expense>> GetExpensesAsync(string userId, bool isAdmin)
    {
        string cacheKey = isAdmin ? "AllExpenses" : $"Expenses_{userId}";
        if (!_cache.TryGetValue(cacheKey, out IEnumerable<Expense>? expenses))
        {
            expenses = isAdmin
                ? await _context.Expenses.ToListAsync()
                : await _context.Expenses.Where(e => e.UserId == userId).ToListAsync();

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5));
            _cache.Set(cacheKey, expenses, cacheOptions);
        }
        return expenses ?? Enumerable.Empty<Expense>();
    }

    public async Task<Expense?> GetExpenseByIdAsync(int id, string userId, bool isAdmin)
    {
        return await _context.Expenses
            .FirstOrDefaultAsync(e => e.Id == id && (isAdmin || e.UserId == userId));
    }

    public async Task<Expense> CreateExpenseAsync(ExpenseDto expenseDto, string userId)
    {
        var expense = new Expense
        {
            UserId = userId,
            Amount = expenseDto.Amount,
            Category = expenseDto.Category,
            Description = SanitizeInput(expenseDto.Description),
            Date = expenseDto.Date
        };
        _context.Expenses.Add(expense);
        await _context.SaveChangesAsync();
        _cache.Remove($"Expenses_{userId}"); // Invalidate cache
        return expense;
    }

    public async Task<Expense?> UpdateExpenseAsync(int id, ExpenseDto expenseDto, string userId, bool isAdmin)
    {
        var expense = await _context.Expenses
            .FirstOrDefaultAsync(e => e.Id == id && (isAdmin || e.UserId == userId));
        if (expense == null) return null;

        expense.Amount = expenseDto.Amount;
        expense.Category = expenseDto.Category;
        expense.Description = SanitizeInput(expenseDto.Description);
        expense.Date = expenseDto.Date;

        await _context.SaveChangesAsync();
        _cache.Remove(isAdmin ? "AllExpenses" : $"Expenses_{userId}");
        return expense;
    }

    public async Task<bool> DeleteExpenseAsync(int id, string userId, bool isAdmin)
    {
        var expense = await _context.Expenses
            .FirstOrDefaultAsync(e => e.Id == id && (isAdmin || e.UserId == userId));
        if (expense == null) return false;

        _context.Expenses.Remove(expense);
        await _context.SaveChangesAsync();
        _cache.Remove(isAdmin ? "AllExpenses" : $"Expenses_{userId}");
        return true;
    }

    private string SanitizeInput(string input)
    {
        return System.Net.WebUtility.HtmlEncode(input); // Basic XSS prevention
    }
}