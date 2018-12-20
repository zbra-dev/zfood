using ZFood.Core.Validators;
using ZFood.Persistence.API;

namespace ZFood.Core.Contexts
{
    public interface IRestaurantServiceContext
    {
        IRestaurantValidatorFactory RestaurantValidatorFactory { get; }

        IRestaurantRepository RestaurantRepository { get; } 
    }
}
