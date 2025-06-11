using StockManagementDemo.API.Models;
using StockManagementDemo.API.Data;
using Microsoft.EntityFrameworkCore;

namespace StockManagementDemo.API.Services
{
    public class AnalyticsLoggerService(ApplicationDbContext context)
    {
        private readonly ApplicationDbContext _context = context;

        public async Task LogAsync(int garmentId, string eventType, int? userId = null, string? metadata = null)
        {
            var log = new GarmentAnalyticsLog
            {
                GarmentId = garmentId,
                Event = eventType,
                UserId = userId,
                Metadata = metadata,
                Timestamp = DateTime.UtcNow
            };

            _context.GarmentAnalyticsLogs.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}