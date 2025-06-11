namespace StockManagementDemo.API.DTOs
{
    public class LenderStatsDto
    {
        public decimal TotalRentalEarnings { get; set; }
        public decimal TotalResaleEarnings { get; set; }
        public int TotalGarments { get; set; }
        public int TotalRentals { get; set; }
        public List<LenderGarmentSummaryDto> GarmentSummaries { get; set; } = [];
    }

    public class LenderGarmentSummaryDto
    {
        public int GarmentId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal RentalPrice { get; set; }
        public decimal? ResalePrice { get; set; }
        public int TimesRented { get; set; }
        public bool IsResaleAvailable { get; set; }
    }
}