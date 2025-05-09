using Microsoft.EntityFrameworkCore;
using StockManagementDemo.API.Data;
using StockManagementDemo.API.Interfaces;
using StockManagementDemo.API.Models;

namespace StockManagementDemo.API.Repositories
{
    public class StockItemRepository : IStockItemRepository
    {
        private readonly ApplicationDbContext _context;

        public StockItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StockItem>> GetAllAsync()
        {
            return await _context.StockItems
                .Include(s => s.Accessories)
                .Include(s => s.Images)
                .AsSplitQuery()
                .ToListAsync();
        }

        public async Task<StockItem?> GetByIdAsync(int id)
        {
            return await _context.StockItems
                .Include(s => s.Accessories)
                .Include(s => s.Images)
                .AsSplitQuery()
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddAsync(StockItem stockItem)
        {
            _context.StockItems.Add(stockItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(StockItem stockItem)
        {
            _context.StockItems.Update(stockItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.StockItems.FindAsync(id);
            if (item != null)
            {
                _context.StockItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.StockItems.AnyAsync(s => s.Id == id);
        }
    }
}