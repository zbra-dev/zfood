using log4net;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using ZFood.Persistence.API;
using ZFood.Persistence.API.Entity;

namespace ZFood.Persistence
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly ZFoodDbContext context;

        private static readonly ILog log = LogManager.GetLogger(typeof(RestaurantRepository));

        public RestaurantRepository(ZFoodDbContext context)
        {
            this.context = context;
        }

        public async Task<RestaurantEntity> FindById(string id)
        {
            return await context.Restaurants.FirstOrDefaultAsync(r => r.Id == id);
        }

        public Task<RestaurantEntity[]> Get(int take, int skip, string query)
        {
            var items = context.Restaurants.Skip(skip).Take(take);
            if (!string.IsNullOrEmpty(query))
            {
                items = items.Where(i => i.Name.StartsWith(query));
            }

            return items.ToArrayAsync();
        }

        public Task<int> GetTotalCount()
        {
            return context.Restaurants.CountAsync();
        }

        public async Task Delete(string id)
        {
            var entry = context.Entry(new RestaurantEntity() { Id = id });
            entry.State = EntityState.Deleted;
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException error)
            {
                log.Error($"{error.Message}");
            }
        }

        public async Task<RestaurantEntity> CreateRestaurant(RestaurantEntity restaurant)
        {
            var createdRestaurant = await context.Restaurants.AddAsync(restaurant);
            await context.SaveChangesAsync();
            return createdRestaurant.Entity;
        }

        public async Task UpdateRestaurant(RestaurantEntity restaurant)
        {
            var entry = context.Entry(restaurant);
            entry.State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<RestaurantEntity> FindRestaurantByNameAndAddress(string name, string address)
        {
            return await context.Restaurants.FirstOrDefaultAsync(r => r.Name == name && r.Address == address);
        }
    }
}
