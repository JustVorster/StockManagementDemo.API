using Microsoft.EntityFrameworkCore;
using StockManagementDemo.API.Models;

namespace StockManagementDemo.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Garment> Garments { get; set; }
        public DbSet<Accessory> Accessories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<RentalPeriod> RentalPeriods { get; set; }
        public DbSet<GarmentAnalyticsLog> GarmentAnalyticsLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Garment>()
                .Property(g => g.RentalPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Garment>()
                .Property(g => g.ResalePrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<RentalPeriod>()
                .HasOne(r => r.Renter)
                .WithMany()
                .HasForeignKey(r => r.RenterId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
