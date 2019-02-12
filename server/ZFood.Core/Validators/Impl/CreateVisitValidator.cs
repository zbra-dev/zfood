using System;
using System.Threading.Tasks;
using ZFood.Core.API;
using ZFood.Core.API.Exceptions;
using ZFood.Model;
using ZFood.Persistence.API;

namespace ZFood.Core.Validators.Impl
{
    public class CreateVisitValidator : IValidator<CreateVisitRequest>
    {
        private readonly IRestaurantRepository restaurantRepository;
        private readonly IUserRepository userRepository;

        public CreateVisitValidator(IRestaurantRepository restaurantRepository, IUserRepository userRepository)
        {
            this.restaurantRepository = restaurantRepository;
            this.userRepository = userRepository;
        }

        public async Task<ValidatorResult> Validate(CreateVisitRequest request)
        {
            var validationResult = new ValidatorResult();
            if (request == null)
            {
                validationResult.Exception = new ArgumentNullException(nameof(request));
                return validationResult;
            }

            var restaurant = await restaurantRepository.FindById(request.RestaurantId);
            if (restaurant == null)
            {
                validationResult.Exception = new EntityNotFoundException(typeof(Restaurant), request.RestaurantId);
                return validationResult;
            }

            var user = await userRepository.FindById(request.UserId);
            if (user == null)
            {
                validationResult.Exception = new EntityNotFoundException(typeof(User), request.UserId);
                return validationResult;
            }

            return validationResult;
        }
    }
}
