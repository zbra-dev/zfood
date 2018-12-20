using ZFood.Core.API;

namespace ZFood.Core.Validators
{
    public interface IRestaurantValidatorFactory
    {
        IValidator<CreateRestaurantRequest> CreateRestaurantCreationValidator();

        IValidator<UpdateRestaurantRequest> CreateUpdateRestaurantValidator();
    }
}
