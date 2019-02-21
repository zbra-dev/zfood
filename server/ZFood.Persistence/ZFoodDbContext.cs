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

            var users = Enumerable.Range(1, 50)
                .Select(n => new UserEntity
                {
                    Id = n.ToString(),
                    Name = $"John Doe {n}",
                    Email = $"john{n}@doe",
                    AvatarUrl = $"http://lorempixel.com/400/400/cats/{n % 10}/",
                    Provider = "Slack",
                    ProviderId = n.ToString(),
                })
                .ToArray();
            modelBuilder.Entity<UserEntity>().HasData(users);

            var restaurants = Enumerable.Range(1, 50)
                .Select(n => new RestaurantEntity
                {
                    Id = n.ToString(),
                    Name = $"Example Restaurant {n}",
                    Address = "Rua das Flores, 27",
                })
                .ToArray();
            modelBuilder.Entity<RestaurantEntity>().HasData(restaurants);

            var visits = Enumerable.Range(1, 50)
                .Select(n => new VisitEntity
                {
                    Id = n.ToString(),
                    UserId = "1",
                    RestaurantId = "1",
                    Rate = 3,
                })
                .ToArray();
            modelBuilder.Entity<VisitEntity>().HasData(visits);
        }
    }
}
