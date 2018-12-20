using ZFood.Core.Validators;
using ZFood.Persistence.API;

namespace ZFood.Core.Contexts
{
    public class RestaurantServiceContext : IRestaurantServiceContext
    {
        public IRestaurantValidatorFactory RestaurantValidatorFactory { get; }
        public IRestaurantRepository RestaurantRepository { get; }

        public RestaurantServiceContext(IRestaurantValidatorFactory restaurantValidatorFactory,
                                        IRestaurantRepository restaurantRepository)
        {
            RestaurantValidatorFactory = restaurantValidatorFactory;
            RestaurantRepository = restaurantRepository;
        }
    }
}
