using StockManagementDemo.API.Models;

namespace StockManagementDemo.API.Interfaces
{
    public interface IGarmentRepository
    {
        Task<IEnumerable<Garment>> GetAllAsync();
        Task<Garment?> GetByIdAsync(int id);
        Task AddAsync(Garment garment);
        Task UpdateAsync(Garment garment);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
