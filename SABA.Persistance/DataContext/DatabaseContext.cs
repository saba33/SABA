using Microsoft.EntityFrameworkCore;
using SABA.Core.Models.ProductModel;
using SABA.Core.Models.RecomendationModel;
using SABA.Core.Models.SaleModel;
using SABA.Core.Models.UserModel;

namespace SABA.Persistance.DataContext
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }
        public DbSet<Product> Products { get; set; }
        //public DbSet<Bonus> Bonuses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // User table configuration
            builder.Entity<User>()
                .HasKey(u => u.UserId);

            // Self-referencing relationship (User → User)
            builder.Entity<User>()
                .HasMany(u => u.RecommendedUsers)
                .WithOne(r => r.Recommender)
                .HasForeignKey(u => u.RecommenderId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade

            // Product
            builder.Entity<Product>()
                .HasKey(p => p.ProductId);

            // Sale
            builder.Entity<Sale>()
                .HasKey(s => s.SaleId);

            builder.Entity<Sale>()
                .HasOne(s => s.Distributor)
                .WithMany(u => u.Sales)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade

            // Recommendation
            builder.Entity<Recommendation>()
                .HasKey(r => r.RecommendationId);

            builder.Entity<Recommendation>()
                .HasOne(r => r.Recommender)
                .WithMany(u => u.Recommendations)
                .HasForeignKey(r => r.RecommenderId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade

            builder.Entity<Recommendation>()
                .HasOne(r => r.RecommendedUser)
                .WithMany()
                .HasForeignKey(r => r.RecommendedUserId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade
        }
    }
}
