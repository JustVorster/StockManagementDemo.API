using System.ComponentModel.DataAnnotations;

namespace StockManagementDemo.API.Models
{
    public class Garment
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        public decimal RentalPrice { get; set; }

        public decimal? ResalePrice { get; set; }

        public bool IsResaleAvailable { get; set; }

        [Required]
        public string Size { get; set; } = string.Empty;

        public string Occasion { get; set; } = string.Empty;

        public ICollection<Image> Images { get; set; } = new List<Image>();

        public ICollection<Accessory> Accessories { get; set; } = new List<Accessory>();

        public ICollection<RentalPeriod> Rentals { get; set; } = new List<RentalPeriod>();

        public int LenderId { get; set; } // Link to User
        public User? Lender { get; set; }
    }
}
