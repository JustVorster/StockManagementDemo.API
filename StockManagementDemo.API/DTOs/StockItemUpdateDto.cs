using System.ComponentModel.DataAnnotations;

namespace StockManagementDemo.API.DTOs
{
    public class StockItemUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string RegNo { get; set; } = string.Empty;

        public string Make { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public int ModelYear { get; set; }

        public int KMS { get; set; }

        public string Colour { get; set; } = string.Empty;

        public string VIN { get; set; } = string.Empty;

        public decimal RetailPrice { get; set; }

        public decimal CostPrice { get; set; }
    }
}