using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StockManagementDemo.API.Data;
using StockManagementDemo.API.DTOs;
using StockManagementDemo.API.Interfaces;
using StockManagementDemo.API.Models;

namespace StockManagementDemo.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public AuthService(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username.ToLower() == username.ToLower());
        }

        public async Task<User> RegisterUserAsync(UserRegisterDto dto)
        {
            var hashed = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = hashed,
                Role = "User"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<string?> AuthenticateUserAsync(UserLoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);
            if (user is null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return null;

            //JWT
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}