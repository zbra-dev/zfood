using System;
using System.Threading.Tasks;
using ZFood.Core.API;
using ZFood.Core.API.Exceptions;
using ZFood.Persistence.API;

namespace ZFood.Core.Validators.Impl
{
    public class UpdateRestaurantValidator : IValidator<UpdateRestaurantRequest>
    {
        private readonly IRestaurantRepository repository;

        public UpdateRestaurantValidator(IRestaurantRepository repository)
        {
            this.repository = repository;
        }

        public async Task<ValidatorResult> Validate(UpdateRestaurantRequest request)
        {
            var validationResult = new ValidatorResult();
            if (request == null)
            {
                validationResult.Exception = new ArgumentNullException(nameof(request));
            }
            else
            {
                var restaurant = await repository.FindById(request.Id);
                if (restaurant == null)
                {
                    validationResult.Exception = new EntityNotFoundException($"Could not find Restaurant {request.Id}");
                }
            }
            return validationResult;
        }
    }
}
