using System;
using System.Threading.Tasks;
using ZFood.Persistence.API;

namespace ZFood.Core.Validators.Impl
{
    class DeleteUserValidator : IValidator<string>
    {
        private readonly IUserRepository userRepository;

        public DeleteUserValidator(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<ValidatorResult> Validate(string id)
        {
            var validationResult = new ValidatorResult();
            if (string.IsNullOrWhiteSpace(id))
            {
                validationResult.Exception = new ArgumentNullException(nameof(id));
                return validationResult;
            }
            return await Task.FromResult(validationResult);
        }
    }
}