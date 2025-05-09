using Microsoft.EntityFrameworkCore;
using StockManagementDemo.API.Models;

namespace StockManagementDemo.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<StockItem> StockItems { get; set; }
        public DbSet<Accessory> Accessories { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StockItem>()
                .Property(s => s.RetailPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<StockItem>()
                .Property(s => s.CostPrice)
                .HasPrecision(18, 2);
        }
    }
}
