using StockManagementDemo.API.Models;

namespace StockManagementDemo.API.Interfaces
{
    public interface IStockItemRepository
    {
        Task<IEnumerable<StockItem>> GetAllAsync();
        Task<StockItem?> GetByIdAsync(int id);
        Task AddAsync(StockItem stockItem);
        Task UpdateAsync(StockItem stockItem);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
