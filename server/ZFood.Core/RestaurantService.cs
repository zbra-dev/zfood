using System;
using System.Linq;
using System.Threading.Tasks;
using ZFood.Core.API;
using ZFood.Core.Contexts;
using ZFood.Core.Extensions;
using ZFood.Model;
using ZFood.Persistence.API;

namespace ZFood.Core
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantServiceContext context;

        public RestaurantService(IRestaurantServiceContext context)
        {
            this.context = context;
        }

        public async Task<Restaurant> FindById(string id)
        {
            var entity = await context.RestaurantRepository.FindById(id);
            return entity?.ToModel();
        }

        public async Task<Page<Restaurant>> Get(int take, int skip, bool count, string query)
        {
            var increasedTake = take + 1;
            var entities = await context.RestaurantRepository.Get(increasedTake, skip, query);
            var restaurants = entities.Select(r => r.ToModel()).ToArray();
            var hasMore = restaurants.Length == increasedTake;
            int? totalCount = null;

            if (count)
            {
                totalCount = await context.RestaurantRepository.GetTotalCount();
            }
            
            return new Page<Restaurant>
            {
                Items = restaurants.Take(take),
                HasMore = hasMore,
                TotalCount = totalCount,
            };
        }

        public async Task Delete(string id)
        {
            await context.RestaurantRepository.Delete(id);
        }

        public async Task<Restaurant> CreateRestaurant(CreateRestaurantRequest restaurantRequest)
        {
            var createdRestaurant = await context.RestaurantRepository.CreateRestaurant(restaurantRequest.ToEntity());
            return createdRestaurant.ToModel();
        }

        public async Task UpdateRestaurant(UpdateRestaurantRequest restaurantRequest)
        {
            await context.RestaurantRepository.UpdateRestaurant(restaurantRequest.ToEntity());
        }
    }
}
