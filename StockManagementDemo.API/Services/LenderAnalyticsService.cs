using Microsoft.EntityFrameworkCore;
using StockManagementDemo.API.Data;
using StockManagementDemo.API.DTOs;
using StockManagementDemo.API.Models;

namespace StockManagementDemo.API.Services
{
    public class LenderAnalyticsService(ApplicationDbContext context)
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<LenderStatsDto> GetStatsAsync(int lenderId)
        {
            var garments = await _context.Garments
                .Include(g => g.Rentals)
                .Where(g => g.LenderId == lenderId)
                .ToListAsync();

            var garmentSummaries = garments.Select(g => new LenderGarmentSummaryDto
            {
                GarmentId = g.Id,
                Name = g.Name,
                RentalPrice = g.RentalPrice,
                ResalePrice = g.ResalePrice,
                TimesRented = g.Rentals.Count(r => r.Status == RentalStatus.Confirmed),
                IsResaleAvailable = g.IsResaleAvailable
            }).ToList();

            return new LenderStatsDto
            {
                TotalGarments = garments.Count,
                TotalRentals = garmentSummaries.Sum(g => g.TimesRented),
                TotalRentalEarnings = garmentSummaries.Sum(g => g.TimesRented * g.RentalPrice),
                TotalResaleEarnings = garments
                    .Where(g => !g.IsResaleAvailable && g.ResalePrice.HasValue)
                    .Sum(g => g.ResalePrice!.Value),
                GarmentSummaries = garmentSummaries
            };
        }
    }
}