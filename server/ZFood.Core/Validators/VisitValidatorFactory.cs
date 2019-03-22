using ZFood.Core.API;
using ZFood.Core.Validators.Impl;
using ZFood.Persistence.API;

namespace ZFood.Core.Validators
{
    public class VisitValidatorFactory : IVisitValidatorFactory
    {
        private readonly IVisitRepository visitRepository;
        private readonly IRestaurantRepository restaurantRepository;
        private readonly IUserRepository userRepository;

        public VisitValidatorFactory(IVisitRepository visitRepository, IRestaurantRepository restaurantRepository, IUserRepository userRepository)
        {
            this.visitRepository = visitRepository;
            this.restaurantRepository = restaurantRepository;
            this.userRepository = userRepository;
        }

        public IValidator<CreateVisitRequest> CreateVisitCreationValidator()
        {
            return new CreateVisitValidator(restaurantRepository, userRepository);
        }

        public IValidator<UpdateVisitRequest> CreateUpdateVisitValidator()
        {
            return new UpdateVisitValidator(visitRepository, restaurantRepository, userRepository);
        }

        public IValidator<string> CreateDeleteVisitValidator()
        {
            return new DeleteVisitValidator(visitRepository);
        }
    }
}
