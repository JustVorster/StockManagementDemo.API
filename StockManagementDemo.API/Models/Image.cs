namespace StockManagementDemo.API.Models
{
    public class Image
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public byte[]? Data { get; set; }

        public int GarmentId { get; set; }

        public Garment? Garment { get; set; }
    }
}
