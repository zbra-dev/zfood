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

        public async Task<Page<Restaurant>> Get(PageRequest request)
        {
            var increasedTake = request.Take + 1;
            var entities = await repository.Get(increasedTake, request.Skip, request.Query);
            var restaurants = entities.Select(r => r.ToModel()).ToArray();
            var hasMore = restaurants.Length == increasedTake;
            int? totalCount = null;

            if (request.Count)
            {
                totalCount = await repository.GetTotalCount();
            }
            
            return new Page<Restaurant>
            {
                Items = restaurants.Take(request.Take),
                HasMore = hasMore,
                TotalCount = totalCount,
            };
        }

        public async Task Delete(string id)
        {
            await repository.Delete(id);
        }

        public async Task<Restaurant> CreateRestaurant(CreateRestaurantRequest restaurantRequest)
        {
            var createdRestaurant = await repository.CreateRestaurant(restaurantRequest.ToEntity());
            return createdRestaurant.ToModel();
        }

        public async Task UpdateRestaurant(UpdateRestaurantRequest restaurantRequest)
        {
            await repository.UpdateRestaurant(restaurantRequest.ToEntity());
        }
    }
}
