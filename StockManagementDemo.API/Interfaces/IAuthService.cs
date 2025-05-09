using StockManagementDemo.API.DTOs;
using StockManagementDemo.API.Models;

namespace StockManagementDemo.API.Interfaces
{
    public interface IAuthService
    {
        Task<bool> UserExistsAsync(string username);
        Task<User> RegisterUserAsync(UserRegisterDto dto);
        Task<string?> AuthenticateUserAsync(UserLoginDto dto);
    }
}
