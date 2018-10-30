using Microsoft.EntityFrameworkCore;
using System.Linq;
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
            var restaurants = Enumerable.Range(1, 50)
                .Select(n => new RestaurantEntity { Id = n.ToString(), Name = $"Example Restaurant {n}", Address = "Rua das Flores, 27" })
                .ToArray();
            modelBuilder.Entity<RestaurantEntity>().HasData(restaurants);

            modelBuilder.Entity<VisitEntity>().HasData(
                new VisitEntity { Id = "1", UserId = "1", RestaurantId = "1", Rate = 3 });
        }
    }
}
