using Microsoft.EntityFrameworkCore;
using StockManagementDemo.API.Data;
using StockManagementDemo.API.Models;

namespace StockManagementDemo.API.Services
{
    public class RentalService(ApplicationDbContext context)
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<(bool IsAvailable, string? Reason)> IsAvailableAsync(int garmentId, DateTime from, DateTime to)
        {
            var garment = await _context.Garments
                .Include(g => g.Rentals)
                .FirstOrDefaultAsync(g => g.Id == garmentId);

            if (garment == null)
                return (false, "Garment not found");

            var overlap = garment.Rentals.Any(r =>
                r.Status == RentalStatus.Confirmed &&
                from < r.RentTo && to > r.RentFrom);

            if (overlap)
                return (false, "Date range is already booked");

            return (true, null);
        }

        public async Task<RentalPeriod?> CreateRentalAsync(int garmentId, int userId, DateTime from, DateTime? to)
        {
            var finalTo = to ?? from.AddDays(7); 

            var (isAvailable, _) = await IsAvailableAsync(garmentId, from, finalTo);
            if (!isAvailable) return null;

            var rental = new RentalPeriod
            {
                GarmentId = garmentId,
                RenterId = userId,
                RentFrom = from,
                RentTo = finalTo,
                Status = RentalStatus.Pending
            };

            _context.RentalPeriods.Add(rental);
            await _context.SaveChangesAsync();
            return rental;
        }
    }
}