using ZFood.Core.API;

namespace ZFood.Core.Validators
{
    public interface IRestaurantValidatorFactory
    {
        IValidator<PageRequest> CreateSearchEntityValidator();

        IValidator<CreateRestaurantRequest> CreateRestaurantCreationValidator();

        IValidator<UpdateRestaurantRequest> CreateUpdateRestaurantValidator();

        IValidator<string> CreateDeleteRestaurantValidator();
    }
}
