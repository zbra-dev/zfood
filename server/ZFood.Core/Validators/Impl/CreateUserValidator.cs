using System;
using System.Threading.Tasks;
using ZFood.Core.API;
using ZFood.Core.API.Exceptions;
using ZFood.Persistence.API;

namespace ZFood.Core.Validators.Impl
{
    public class CreateUserValidator : IValidator<CreateUserRequest>
    {
        private readonly IUserRepository repository;

        public CreateUserValidator(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<ValidatorResult> Validate(CreateUserRequest request)
        {

            var validationResult = new ValidatorResult();
            if (request == null)
            {
                validationResult.Exception = new ArgumentNullException(nameof(request));
                return validationResult;
            }

            var email = await repository.FindByEmail(request.Email);
            if (email != null)
            {
                validationResult.Exception = new EntityAlreadyExistsException($"Email {request.Email} already exists");
                return validationResult;
            }

            var username = await repository.FindByUsername(request.Username);
            if (username != null)
            {
                validationResult.Exception = new EntityAlreadyExistsException($"Username {request.Username} already exists");
                return validationResult;
            }

            return validationResult;
        }
    }
}
