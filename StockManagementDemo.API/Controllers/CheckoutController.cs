using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockManagementDemo.API.DTOs;
using StockManagementDemo.API.Services;
using System.Security.Claims;

namespace StockManagementDemo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CheckoutController(GarmentService garmentService, AnalyticsLoggerService analyticsLogger) : ControllerBase
    {
        private readonly GarmentService _garmentService = garmentService;
        private readonly AnalyticsLoggerService _analyticsLogger = analyticsLogger;

        [HttpPost]
        public async Task<IActionResult> BuyNow([FromBody] CheckoutRequestDto dto)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdStr, out var userId))
                return Unauthorized("Invalid user ID");

            var (isValid, error, garment) = await _garmentService.GetResaleGarmentAsync(dto.GarmentId);
            if (!isValid)
            {
                await _analyticsLogger.LogAsync(dto.GarmentId, "Failed resale attempt", userId);
                return BadRequest(error);
            }

            await _analyticsLogger.LogAsync(dto.GarmentId, "Resold", userId, $"{{ \"price\": {garment!.ResalePrice} }}");


            // Placeholder for external payment gateway integration 
            return Ok(new
            {
                Message = "Checkout initiated",
                BuyerId = userId,
                GarmentId = garment.Id,
                Amount = garment.ResalePrice,
                RedirectUrl = $"https://xyzabc={garment.ResalePrice}&ref=garment-{garment.Id}"
            });
        }
    }
}
