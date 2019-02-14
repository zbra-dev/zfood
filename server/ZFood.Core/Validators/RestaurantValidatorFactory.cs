using ZFood.Core.API;
using ZFood.Core.Validators.Impl;
using ZFood.Persistence.API;

namespace ZFood.Core.Validators
{
    public class RestaurantValidatorFactory : IRestaurantValidatorFactory
    {
        private readonly IRestaurantRepository repository;

        public RestaurantValidatorFactory(IRestaurantRepository repository)
        {
            this.repository = repository;
        }

        public IValidator<CreateRestaurantRequest> CreateRestaurantCreationValidator()
        {
            return new CreateRestaurantValidator(repository);
        }

        public IValidator<UpdateRestaurantRequest> CreateUpdateRestaurantValidator()
        {
            return new UpdateRestaurantValidator(repository);
        }
    }
}
