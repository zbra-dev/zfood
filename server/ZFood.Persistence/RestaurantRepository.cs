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
    }
}
