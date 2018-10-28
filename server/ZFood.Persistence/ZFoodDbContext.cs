using Microsoft.EntityFrameworkCore;
using ZFood.Persistence.API.Entity;

namespace ZFood.Persistence
{
    public class ZFoodDbContext : DbContext
    {
        public DbSet<RestaurantEntity> Restaurants { get; set; }

        public ZFoodDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RestaurantEntity>().HasData(
                new RestaurantEntity { Id = "1", Name = "Example Restaurant 1" },
                new RestaurantEntity { Id = "2", Name = "Example Restaurant 2" });
        }
    }
}
