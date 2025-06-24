using ExpenseTrackerApi.DTOs;
using ExpenseTrackerApi.Models;

namespace ExpenseTrackerApi.Services;

public interface IExpenseService
{
    Task<IEnumerable<Expense>> GetExpensesAsync(string userId, bool isAdmin);
    Task<Expense?> GetExpenseByIdAsync(int id, string userId, bool isAdmin);
    Task<Expense> CreateExpenseAsync(ExpenseDto expenseDto, string userId);
    Task<Expense?> UpdateExpenseAsync(int id, ExpenseDto expenseDto, string userId, bool isAdmin);
    Task<bool> DeleteExpenseAsync(int id, string userId, bool isAdmin);
}
