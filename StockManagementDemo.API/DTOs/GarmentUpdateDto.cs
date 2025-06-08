using System.ComponentModel.DataAnnotations;

namespace StockManagementDemo.API.DTOs
{
    public class GarmentUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        public string Size { get; set; } = string.Empty;

        public string Occasion { get; set; } = string.Empty;

        [Required]
        public decimal RentalPrice { get; set; }

        public decimal? ResalePrice { get; set; }

        public bool IsResaleAvailable { get; set; }
    }
}
