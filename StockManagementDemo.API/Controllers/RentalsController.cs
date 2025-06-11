using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockManagementDemo.API.DTOs;
using StockManagementDemo.API.Models;
using StockManagementDemo.API.Services;
using System.Security.Claims;

namespace StockManagementDemo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RentalsController(RentalService rentalService, AnalyticsLoggerService analyticsLogger) : ControllerBase
    {
        private readonly RentalService _rentalService = rentalService;
        private readonly AnalyticsLoggerService _analyticsLogger = analyticsLogger;

        [HttpPost]
        public async Task<IActionResult> RentGarment([FromBody] RentalRequestDto dto)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
                

            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdStr, out var userId))
                return Unauthorized("Invalid user ID");

            var rentTo = dto.RentTo ?? dto.RentFrom.AddDays(7);

            var (isAvailable, reason) = await _rentalService.IsAvailableAsync(dto.GarmentId, dto.RentFrom, rentTo);
            if (!isAvailable) {
                return BadRequest(reason);
            }


            var rental = await _rentalService.CreateRentalAsync(dto.GarmentId, userId, dto.RentFrom, dto.RentTo);
            if (rental == null) {
                await _analyticsLogger.LogAsync(dto.GarmentId, "Unable to create rental transaction", userId);
                return BadRequest("Unable to create rental");
            }
               

            await _analyticsLogger.LogAsync(dto.GarmentId, "Rented", userId);

            return Ok(new { rental.Id, Status = rental.Status.ToString() });
        }
    }
}