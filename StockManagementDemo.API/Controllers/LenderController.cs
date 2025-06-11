using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockManagementDemo.API.Services;
using System.Security.Claims;

namespace StockManagementDemo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "User,Lender,Admin")]
    public class LenderController(LenderAnalyticsService analyticsService) : ControllerBase
    {
        private readonly LenderAnalyticsService _analyticsService = analyticsService;

        [HttpGet("stats")]
        public async Task<IActionResult> GetStats([FromServices] AnalyticsLoggerService analyticsLogger)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdStr, out var lenderId))
                return Unauthorized("Invalid user ID");

            await analyticsLogger.LogAsync(-1, "Lender stats viewed", lenderId);
            var stats = await _analyticsService.GetStatsAsync(lenderId);
            return Ok(stats);
        }

    }
}