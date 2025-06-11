using StockManagementDemo.API.Models;

namespace StockManagementDemo.API.DTOs
{
    public class DetailedRentalCalendarDto
    {
        public int RentalId { get; set; }
        public int RenterId { get; set; }
        public DateTime RentFrom { get; set; }
        public DateTime RentTo { get; set; }
        public DateTime BufferUntil { get; set; }
        public RentalStatus Status { get; set; }

        // props for future use
        public bool IsCurrent => RentFrom <= DateTime.UtcNow && BufferUntil >= DateTime.UtcNow; //TODO: fix
        public int TotalDays => (BufferUntil - RentFrom).Days;
        public int RentalDurationDays => (RentTo - RentFrom).Days;
        public int BufferDays => (BufferUntil - RentTo).Days;
    }
}