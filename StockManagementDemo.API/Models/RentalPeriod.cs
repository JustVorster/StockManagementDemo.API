namespace StockManagementDemo.API.Models
{
    public class RentalPeriod
    {
        public int Id { get; set; }

        public DateTime RentFrom { get; set; }
        public DateTime RentTo { get; set; }

        public RentalStatus Status { get; set; } = RentalStatus.Pending;

        public int GarmentId { get; set; }
        public Garment? Garment { get; set; }

        public int RenterId { get; set; }
        public User? Renter { get; set; }
    }

    public enum RentalStatus
    {
        Pending,
        Confirmed,
        Cancelled
    }
}
