using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace StockManagementDemo.API.DTOs
{
    public class ImageUploadDto
    {
        [Required]
        public IFormFile? File { get; set; }

        [Required]
        public int StockItemId { get; set; }
    }
}
