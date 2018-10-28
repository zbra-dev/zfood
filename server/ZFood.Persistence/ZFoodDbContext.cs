using Microsoft.EntityFrameworkCore;
using ZFood.Persistence.API.Entity;

namespace ZFood.Persistence
{
    public class ZFoodDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public DbSet<RestaurantEntity> Restaurants { get; set; }

        public DbSet<VisitEntity> Visits { get; set; }

        public ZFoodDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity { Id = "1", Name = "John Doe" });
            modelBuilder.Entity<RestaurantEntity>().HasData(
                new RestaurantEntity { Id = "1", Name = "Example Restaurant 1" },
                new RestaurantEntity { Id = "2", Name = "Example Restaurant 2" });
            modelBuilder.Entity<VisitEntity>().HasData(
                new VisitEntity { Id = "1", UserId = "1", RestaurantId = "1", Rate = 3 });
        }
    }
}
