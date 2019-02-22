using System;
using System.Threading.Tasks;
using ZFood.Core.API;
using ZFood.Core.API.Exceptions;
using ZFood.Model;
using ZFood.Persistence.API;

namespace ZFood.Core.Validators.Impl
{
    public class CreateRestaurantValidator : IValidator<CreateRestaurantRequest>
    {
        private readonly IRestaurantRepository repository;

        public CreateRestaurantValidator(IRestaurantRepository repository)
        {
            this.repository = repository;
        }

        public async Task<ValidatorResult> Validate(CreateRestaurantRequest request)
        {
            var validationResult = new ValidatorResult();
            if (request == null)
            {
                validationResult.Exception = new ArgumentNullException(nameof(request));
                return validationResult;
            }

            var restaurant = await repository.FindRestaurantByNameAndAddress(request.Name, request.Address);
            if (restaurant != null)
            {
                var restaurantValuePairs = new[]
                {
                    new EntityValuePair(nameof(Restaurant.Name), request.Name),
                    new EntityValuePair(nameof(Restaurant.Address), request.Address)
                };
                validationResult.Exception = new EntityAlreadyExistsException(typeof(Restaurant), restaurantValuePairs);
                return validationResult;
            }

            return validationResult;
        }
    }
}
