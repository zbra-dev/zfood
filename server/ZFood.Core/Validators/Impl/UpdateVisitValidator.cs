using System;
using System.Threading.Tasks;
using ZFood.Core.API;
using ZFood.Core.API.Exceptions;
using ZFood.Persistence.API;

namespace ZFood.Core.Validators.Impl
{
    public class UpdateVisitValidator : IValidator<UpdateVisitRequest>
    {
        private readonly IVisitRepository visitRepository;
        private readonly IRestaurantRepository restaurantRepository;
        private readonly IUserRepository userRepository;

        public UpdateVisitValidator(IVisitRepository visitRepository, IRestaurantRepository restaurantRepository,
                                    IUserRepository userRepository)
        {
            this.visitRepository = visitRepository;
            this.restaurantRepository = restaurantRepository;
            this.userRepository = userRepository;
        }

        public Task<ValidatorResult> Validate(UpdateVisitRequest request)
        {
            return Task.Factory.StartNew(() =>
            {
                var validationResult = new ValidatorResult();
                if (request == null)
                {
                    validationResult.Exception = new ArgumentNullException(nameof(request));
                    return validationResult;
                }

                var visit = visitRepository.FindById(request.Id);
                if (visit == null)
                {
                    validationResult.Exception = new EntityNotFoundException($"Could not find Visit {request.Id}");
                    return validationResult;
                }

                var restaurant = restaurantRepository.FindById(request.RestaurantId);
                if (restaurant == null)
                {
                    validationResult.Exception = new EntityNotFoundException($"Could not find Restaurant {request.RestaurantId}");
                    return validationResult;
                }

                var user = userRepository.FindById(request.UserId);
                if (user == null)
                {
                    validationResult.Exception = new EntityNotFoundException($"Could not find User {request.UserId}");
                    return validationResult;
                }

                return validationResult;
            });
        }
    }
}
