namespace StockManagementDemo.API.DTOs
{
    public class GarmentReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public string Size { get; set; } = string.Empty;
        public string Occasion { get; set; } = string.Empty;

        public decimal RentalPrice { get; set; }
        public decimal? ResalePrice { get; set; }
        public bool IsResaleAvailable { get; set; }

        public int LenderId { get; set; }
    }
}
