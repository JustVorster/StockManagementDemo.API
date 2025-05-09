using Microsoft.EntityFrameworkCore;
using StockManagementDemo.API.Interfaces;
using StockManagementDemo.API.Repositories;
using StockManagementDemo.API.Services;
using StockManagementDemo.API.Data;
using StockManagementDemo.API.Middleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using StockManagementDemo.API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services 
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IStockItemRepository, StockItemRepository>();
builder.Services.AddScoped<StockItemService>();

// SQL Server 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// CORS for Angular (localhost:4200)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularClient",
        builder => builder.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(e => e.Value?.Errors.Count > 0)
            .SelectMany(x => x.Value!.Errors)
            .Select(e => e.ErrorMessage)
            .ToArray();

        return new BadRequestObjectResult(new { errors });
    };
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = builder.Configuration["Jwt:Key"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "StockManagementDemo API v1");
    c.DocumentTitle = "Stock Management API";
    c.InjectStylesheet("/swagger-ui/dark.css"); 
});

app.UseCors("AllowAngularClient");
//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

using(var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();

    if (!db.StockItems.Any())
    {
        var hilux = new StockItem
        {
            RegNo = "CA123456",
            Make = "Toyota",
            Model = "Hilux",
            ModelYear = 2020,
            KMS = 35000,
            Colour = "White",
            VIN = "ABC123DEF456",
            RetailPrice = 299999,
            CostPrice = 250000,
            DTCreated = DateTime.UtcNow,
            DTUpdated = DateTime.UtcNow,
            Accessories = new List<Accessory>
            {
                new Accessory { Name = "Towbar", Description = "Heavy duty" },
                new Accessory { Name = "Rubber Mats", Description = "Front + back" }
            }
        };

        var ranger = new StockItem
        {
            RegNo = "CF654321",
            Make = "Ford",
            Model = "Ranger",
            ModelYear = 2019,
            KMS = 42000,
            Colour = "Blue",
            VIN = "XYZ654DEF321",
            RetailPrice = 279999,
            CostPrice = 230000,
            DTCreated = DateTime.UtcNow,
            DTUpdated = DateTime.UtcNow,
            Accessories = new List<Accessory>
            {
                new Accessory { Name = "Roll bar", Description = "Black finish" }
            }
        };

        db.StockItems.AddRange(hilux, ranger);
        db.SaveChanges();
    }

    if (!db.Users.Any())
    {
        var passwordHasher = new BCrypt.Net.BCrypt();
        if (!db.Users.Any())
        {
            // Replace the problematic line with the correct method call
            var passwordHash = BCrypt.Net.BCrypt.HashPassword("Demo1234!");
            _ = db.Users.Add(new User
            {
                Username = "demo",
                Email = "demo@stock.com",
                PasswordHash = passwordHash,
                Role = "User"
            });

            db.SaveChanges();
        }

        db.SaveChanges();
    }
}

app.Run();

