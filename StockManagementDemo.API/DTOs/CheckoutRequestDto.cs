using System.ComponentModel.DataAnnotations;

namespace StockManagementDemo.API.DTOs
{
    public class CheckoutRequestDto
    {
        [Required]
        public int GarmentId { get; set; }
    }
}