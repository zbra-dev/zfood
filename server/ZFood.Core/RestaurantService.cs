using System;
using System.Linq;
using System.Threading.Tasks;
using ZFood.Core.API;
using ZFood.Core.Extensions;
using ZFood.Model;
using ZFood.Persistence.API;

namespace ZFood.Core
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository repository;

        public RestaurantService(IRestaurantRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Restaurant> FindById(string id)
        {
            var entity = await repository.FindById(id);
            return entity?.ToModel();
        }

        public async Task<Page<Restaurant>> Get(int take, int skip, bool count, string query)
        {
            var increasedTake = take + 1;
            var entities = await repository.Get(increasedTake, skip, query);
            var restaurants = entities.Select(r => r.ToModel()).ToArray();
            var hasMore = restaurants.Length == increasedTake;
            int? totalCount = null;

            if (count)
            {
                totalCount = await repository.GetTotalCount();
            }
            
            return new Page<Restaurant>
            {
                Items = restaurants.Take(take),
                HasMore = hasMore,
                TotalCount = totalCount,
            };
        }

        public async Task<Restaurant> CreateRestaurant(CreateRestaurantRequest restaurant)
        {
            if (restaurant == null)
            {
                throw new ArgumentNullException(nameof(restaurant));
            }

            var createdRestaurant = await repository.CreateRestaurant(restaurant.ToEntity());
            return createdRestaurant.ToModel();
        }
    }
}
