using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AgricultureEnergyConnect.Data;
using AgricultureEnergyConnect.Models;
using System.Linq;

namespace AgricultureEnergyConnect.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ForumPost> ForumPosts { get; set; }

        public DbSet<Farmer> Farmers { get; set; }
      
        public DbSet<FarmerProduct> FarmerProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FarmerProduct>()
                .HasKey(fp => new { fp.FarmerProductId, fp.ProductId });

            modelBuilder.Entity<FarmerProduct>()
                .HasOne(fp => fp.Farmer)
                .WithMany(f => f.FarmerProducts)
                .HasForeignKey(fp => fp.FarmerProductId);

        }
    }


}
