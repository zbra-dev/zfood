using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZFood.Core.API;
using ZFood.Core.API.Exceptions;
using ZFood.Persistence.API;

namespace ZFood.Core.Validators.Impl
{
    public class UpdateUserValidator : IValidator<UpdateUserRequest>
    {
        private readonly IUserRepository repository;

        public UpdateUserValidator(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<ValidatorResult> Validate(UpdateUserRequest request)
        {
            var validationResult = new ValidatorResult();
            if (request == null)
            {
                validationResult.Exception = new ArgumentNullException(nameof(request));
                return validationResult;
            }

            var user = await repository.FindById(request.Id);
            if (user == null)
            {
                validationResult.Exception = new EntityNotFoundException($"Could not find User {request.Id}");
                return validationResult;
            }

            return validationResult;
        }
    }
}
