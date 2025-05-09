using System.ComponentModel.DataAnnotations;

namespace StockManagementDemo.API.Models
{
    public class Accessory
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int StockItemId { get; set; }

        public StockItem? StockItem { get; set; }
    }
}