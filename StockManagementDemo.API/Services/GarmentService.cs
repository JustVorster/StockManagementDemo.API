using StockManagementDemo.API.Data;
using StockManagementDemo.API.Models;

public class GarmentService(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public async Task<(bool IsValid, string? Error, Garment? Garment)> GetResaleGarmentAsync(int garmentId)
    {
        var garment = await _context.Garments.FindAsync(garmentId);
        if (garment == null)
            return (false, "Garment not found", null);

        if (!garment.IsResaleAvailable || garment.ResalePrice == null)
            return (false, "Garment is not available for resale", null);

        return (true, null, garment);
    }
}