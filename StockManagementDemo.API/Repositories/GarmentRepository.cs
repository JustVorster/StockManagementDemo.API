using Microsoft.EntityFrameworkCore;
using StockManagementDemo.API.Data;
using StockManagementDemo.API.Interfaces;
using StockManagementDemo.API.Models;

namespace StockManagementDemo.API.Repositories
{
    public class GarmentRepository : IGarmentRepository
    {
        private readonly ApplicationDbContext _context;

        public GarmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Garment>> GetAllAsync()
        {
            return await _context.Garments
                .Include(g => g.Accessories)
                .Include(g => g.Images)
                .AsSplitQuery()
                .ToListAsync();
        }

        public async Task<Garment?> GetByIdAsync(int id)
        {
            return await _context.Garments
                .Include(g => g.Accessories)
                .Include(g => g.Images)
                .AsSplitQuery()
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task AddAsync(Garment garment)
        {
            _context.Garments.Add(garment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Garment garment)
        {
            _context.Garments.Update(garment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.Garments.FindAsync(id);
            if (item != null)
            {
                _context.Garments.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Garments.AnyAsync(g => g.Id == id);
        }
    }
}
