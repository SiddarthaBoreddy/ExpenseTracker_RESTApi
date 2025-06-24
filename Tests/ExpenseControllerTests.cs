using ExpenseTrackerApi.Controllers;
using ExpenseTrackerApi.Models;
using ExpenseTrackerApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using Xunit;

namespace ExpenseTrackerApi.Tests;

public class ExpensesControllerTests
{
    private readonly Mock<IExpenseService> _expenseServiceMock;
    private readonly ExpensesController _controller;

    public ExpensesControllerTests()
    {
        _expenseServiceMock = new Mock<IExpenseService>();
        _controller = new ExpensesController(_expenseServiceMock.Object);

        // Mock user claims
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, "test-user-id"),
            new Claim(ClaimTypes.Role, "User")
        };
        var identity = new ClaimsIdentity(claims, "TestAuthType");
        var principal = new ClaimsPrincipal(identity);
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = principal }
        };
    }

    [Fact]
    public async Task GetExpenses_ReturnsOkResult_WithExpenses()
    {
        // Arrange
        var expenses = new List<Expense>
        {
            new Expense { Id = 1, UserId = "test-user-id", Amount = 50, Category = "Food", Description = "Lunch", Date = DateTime.Now }
        };
        _expenseServiceMock.Setup(s => s.GetExpensesAsync("test-user-id", false))
            .ReturnsAsync(expenses);

        // Act
        var result = await _controller.GetExpenses();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnExpenses = Assert.IsAssignableFrom<IEnumerable<Expense>>(okResult.Value);
        Assert.Single(returnExpenses);
    }
}
