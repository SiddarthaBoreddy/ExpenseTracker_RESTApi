using ExpenseTrackerApi.DTOs;

namespace ExpenseTrackerApi.Services;

public interface IAuthService
{
    Task<string> RegisterAsync(RegisterDto registerDto);
    Task<string?> LoginAsync(LoginDto loginDto);
}