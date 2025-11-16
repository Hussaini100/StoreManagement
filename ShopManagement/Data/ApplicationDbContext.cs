using Microsoft.EntityFrameworkCore;
using ShopManagement.Models;

namespace ShopManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // تنظیم precision برای decimalها
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2); // 18 رقم کل، 2 رقم اعشار

            // Seed data
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "لپ تاپ ایسوس ROG",
                    Description = "لپ تاپ گیمینگ با پردازنده Core i7 و کارت گرافیک RTX 4060",
                    Price = 75000000m,
                    StockQuantity = 5,
                    Category = "الکترونیک",
                    ImageUrl = "/images/laptop.jpg",
                    CreatedDate = new DateTime(2024, 1, 1),
                    UpdatedDate = new DateTime(2024, 1, 1),
                    IsActive = true
                },
                new Product
                {
                    Id = 2,
                    Name = "ماوس لاجیتک G502",
                    Description = "ماوس گیمینگ با قابلیت تنظیم وزن و RGB",
                    Price = 2500000m,
                    StockQuantity = 15,
                    Category = "الکترونیک",
                    ImageUrl = "/images/mouse.jpg",
                    CreatedDate = new DateTime(2024, 1, 1),
                    UpdatedDate = new DateTime(2024, 1, 1),
                    IsActive = true
                },
                new Product
                {
                    Id = 3,
                    Name = "کیبورد مکانیکال",
                    Description = "کیبورد مکانیکال با سوییچ Brown و نور RGB",
                    Price = 4500000m,
                    StockQuantity = 8,
                    Category = "الکترونیک",
                    ImageUrl = "/images/keyboard.jpg",
                    CreatedDate = new DateTime(2024, 1, 1),
                    UpdatedDate = new DateTime(2024, 1, 1),
                    IsActive = true
                }
            );
        }
    }
}