using ExpenseTrackerApi.DTOs;
using ExpenseTrackerApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExpenseTrackerApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{
    private readonly IExpenseService _expenseService;

    public ExpensesController(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    [HttpGet]
    public async Task<IActionResult> GetExpenses()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var isAdmin = User.IsInRole("Admin");
        var expenses = await _expenseService.GetExpensesAsync(userId, isAdmin);
        return Ok(expenses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetExpense(int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var isAdmin = User.IsInRole("Admin");
        var expense = await _expenseService.GetExpenseByIdAsync(id, userId, isAdmin);
        if (expense == null) return NotFound();
        return Ok(expense);
    }

    [HttpPost]
    public async Task<IActionResult> CreateExpense([FromBody] ExpenseDto expenseDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var expense = await _expenseService.CreateExpenseAsync(expenseDto, userId);
        return CreatedAtAction(nameof(GetExpense), new { id = expense.Id }, expense);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExpense(int id, [FromBody] ExpenseDto expenseDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var isAdmin = User.IsInRole("Admin");
        var expense = await _expenseService.UpdateExpenseAsync(id, expenseDto, userId, isAdmin);
        if (expense == null) return NotFound();
        return Ok(expense);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExpense(int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var isAdmin = User.IsInRole("Admin");
        var result = await _expenseService.DeleteExpenseAsync(id, userId, isAdmin);
        if (!result) return NotFound();
        return NoContent();
    }
}