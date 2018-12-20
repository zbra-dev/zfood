using System.Threading.Tasks;
using ZFood.Core.API;
using ZFood.Core.Contexts;
using ZFood.Core.Utils;
using ZFood.Model;

namespace ZFood.Core.Decorator
{
    public class RestaurantValidatorDecorator : IRestaurantService
    {
        private readonly IRestaurantService restaurantService;
        private readonly IRestaurantServiceContext context;

        public RestaurantValidatorDecorator(IRestaurantService restaurantService, IRestaurantServiceContext context)
        {
            this.restaurantService = restaurantService;
            this.context = context;
        }

        public async Task<Restaurant> CreateRestaurant(CreateRestaurantRequest restaurant)
        {
            await context.RestaurantValidatorFactory
                         .CreateRestaurantCreationValidator()
                         .ThrowIfNotValid(restaurant);         

            return await restaurantService.CreateRestaurant(restaurant);
        }

        public Task Delete(string id)
        {
            return restaurantService.Delete(id);
        }

        public async Task<Restaurant> FindById(string id)
        {
            var restaurant = await restaurantService.FindById(id);
            return restaurant;
        }

        public Task<Page<Restaurant>> Get(int take, int skip, bool count, string query)
        {
            return restaurantService.Get(take, skip, count, query);
        }

        public async Task UpdateRestaurant(UpdateRestaurantRequest restaurantRequest)
        {
            await context.RestaurantValidatorFactory
                         .CreateUpdateRestaurantValidator()
                         .ThrowIfNotValid(restaurantRequest);

            await restaurantService.UpdateRestaurant(restaurantRequest);
        }
    }
}
