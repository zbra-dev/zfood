using System.Threading.Tasks;
using ZFood.Core.API;
using ZFood.Core.Utils;
using ZFood.Core.Validators;
using ZFood.Model;

namespace ZFood.Core.Decorator
{
    public class RestaurantValidatorDecorator : IRestaurantService
    {
        private readonly IRestaurantService restaurantService;
        private readonly IRestaurantValidatorFactory restaurantValidatorFactory;
        private readonly IPageRequestValidatorFactory pageRequestValidatorFactory;

        public RestaurantValidatorDecorator(IRestaurantService restaurantService, 
            IRestaurantValidatorFactory restaurantValidatorFactory,
            IPageRequestValidatorFactory pageRequestValidatorFactory)
        {
            this.restaurantService = restaurantService;
            this.restaurantValidatorFactory = restaurantValidatorFactory;
            this.pageRequestValidatorFactory = pageRequestValidatorFactory;
        }

        public async Task<Restaurant> CreateRestaurant(CreateRestaurantRequest restaurant)
        {
            await restaurantValidatorFactory.CreateRestaurantCreationValidator().ThrowIfNotValid(restaurant);        
            return await restaurantService.CreateRestaurant(restaurant);
        }

        public async Task Delete(string id)
        {
            await restaurantValidatorFactory.CreateDeleteRestaurantValidator().ThrowIfNotValid(id);
            await restaurantService.Delete(id);
        }

        public async Task<Restaurant> FindById(string id)
        {
            var restaurant = await restaurantService.FindById(id);
            return restaurant;
        }

        public async Task<Page<Restaurant>> Get(PageRequest pageRequest)
        {
            await pageRequestValidatorFactory.CreatePageRequestValidator().ThrowIfNotValid(pageRequest);
            return await restaurantService.Get(pageRequest);
        }

        public async Task UpdateRestaurant(UpdateRestaurantRequest restaurantRequest)
        {
            await restaurantValidatorFactory.CreateUpdateRestaurantValidator().ThrowIfNotValid(restaurantRequest);
            await restaurantService.UpdateRestaurant(restaurantRequest);
        }
    }
}
