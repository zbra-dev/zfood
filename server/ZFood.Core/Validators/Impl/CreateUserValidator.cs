using System;
using System.Threading.Tasks;
using ZFood.Core.API;
using ZFood.Core.API.Exceptions;
using ZFood.Model;
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

            var userFoundByEmail = await repository.FindByEmail(request.Email);
            if (userFoundByEmail != null)
            {
                validationResult.Exception = new EntityAlreadyExistsException(typeof(User), nameof(User.Email), request.Email);
                return validationResult;
            }

            if (!Enum.TryParse<CredentialsProvider>(request.Provider, out var provider))
            {
                validationResult.Exception = new InvalidValueException(typeof(CredentialsProvider), request.Provider);
                return validationResult;
            }

            var userFoundByProviderId = await repository.FindByProviderId(request.Provider, request.ProviderId);
            if (userFoundByProviderId != null)
            {
                var valuePairs = new[]
                {
                    new EntityValuePair(nameof(User.Credentials.Provider), request.Provider),
                    new EntityValuePair(nameof(User.Credentials.ProviderId), request.ProviderId),
                };
                validationResult.Exception = new EntityAlreadyExistsException(typeof(User), valuePairs);
                return validationResult;
            }

            return validationResult;
        }
    }
}
