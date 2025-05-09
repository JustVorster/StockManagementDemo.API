using System.ComponentModel.DataAnnotations;

namespace StockManagementDemo.API.Models
{
    public class StockItem
    {
        public int Id { get; set; }

        [Required]
        public string RegNo { get; set; } = string.Empty;

        [Required]
        public string Make { get; set; } = string.Empty;

        [Required]
        public string Model { get; set; } = string.Empty;

        [Required]
        public int ModelYear { get; set; }

        public int KMS { get; set; }

        public string Colour { get; set; } = string.Empty;

        public string VIN { get; set; } = string.Empty;

        public decimal RetailPrice { get; set; }

        public decimal CostPrice { get; set; }

        public DateTime DTCreated { get; set; } = DateTime.UtcNow;

        public DateTime DTUpdated { get; set; } = DateTime.UtcNow;

        public ICollection<Accessory> Accessories { get; set; } = new List<Accessory>();

        public ICollection<Image> Images { get; set; } = new List<Image>();
    }
}