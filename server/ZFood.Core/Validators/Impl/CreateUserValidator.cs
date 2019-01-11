using System;
using System.Threading.Tasks;
using ZFood.Core.API;

namespace ZFood.Core.Validators.Impl
{
    public class CreateUserValidator : IValidator<CreateUserRequest>
    {
        public async Task<ValidatorResult> Validate(CreateUserRequest request)
        {
            var validationResult = new ValidatorResult();
            if (request == null)
            {
                validationResult.Exception = new ArgumentNullException(nameof(request));
                return validationResult;
            }

            return validationResult;
        }
    }
}
