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

        public async Task<Page<Restaurant>> Get(int take, int skip, string query)
        {
            var increaseTake = take + 1;

            var restaurants = (await repository.Get(increaseTake, skip, query))
                .Select(r => r.ToModel());

            var hasMore = restaurants.Count() == increaseTake;
            
            return new Page<Restaurant>
            {
                Items = restaurants.Take(take),
                HasMore = hasMore,
                TotalCount = 0,
            };
        }
    }
}
