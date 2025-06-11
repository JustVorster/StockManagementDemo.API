using System.ComponentModel.DataAnnotations;

namespace StockManagementDemo.API.DTOs
{
    public class RentalRequestDto
    {
        [Required]
        public int GarmentId { get; set; }

        [Required]
        public DateTime RentFrom { get; set; }

        [Required]
        public DateTime? RentTo { get; set; }
    }
}
