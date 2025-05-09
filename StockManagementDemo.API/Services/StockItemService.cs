using StockManagementDemo.API.Interfaces;
using StockManagementDemo.API.Models;

namespace StockManagementDemo.API.Services
{
    public class StockItemService
    {
        private readonly IStockItemRepository _repository;

        public StockItemService(IStockItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<StockItem>> GetAllAsync() => await _repository.GetAllAsync();
        public async Task<StockItem?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);
        public async Task AddAsync(StockItem item) => await _repository.AddAsync(item);
        public async Task UpdateAsync(StockItem item) => await _repository.UpdateAsync(item);
        public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);
    }
}