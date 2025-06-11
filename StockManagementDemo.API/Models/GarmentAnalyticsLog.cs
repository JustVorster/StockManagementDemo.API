using StockManagementDemo.API.Models;

public class GarmentAnalyticsLog
{
    public int Id { get; set; }
    public int GarmentId { get; set; }
    public int? UserId { get; set; }
    public string Event { get; set; } = string.Empty;  // "Viewed", "Filtered", "Rented", "Resold"
    public string? Metadata { get; set; }              // JSON or details for algorithm
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public Garment? Garment { get; set; }
}