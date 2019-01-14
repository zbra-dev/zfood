using ZFood.Core.API;
using ZFood.Core.Validators.Impl;
using ZFood.Persistence.API;

namespace ZFood.Core.Validators
{
    public class UserValidatorFactory : IUserValidatorFactory
    {
        private readonly IUserRepository repository;

        public UserValidatorFactory(IUserRepository repository)
        {
            this.repository = repository;
        }

        public IValidator<CreateUserRequest> CreateUserCreationValidator()
        {
            return new CreateUserValidator(repository);
        }

        public IValidator<UpdateUserRequest> CreateUpdateUserValidator()
        {
            return new UpdateUserValidator(repository);
        }
    }
}
