using System;
using System.Threading.Tasks;
using ZFood.Core.API;
using ZFood.Core.API.Exceptions;
using ZFood.Model;
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

        public async Task<ValidatorResult> Validate(UpdateUserRequest updateUserRequest)
        {
            var validationResult = new ValidatorResult();
            if (updateUserRequest == null)
            {
                validationResult.Exception = new ArgumentNullException(nameof(updateUserRequest));
                return validationResult;
            }

            var user = await repository.FindById(updateUserRequest.Id);
            if (user == null)
            {
                validationResult.Exception = new EntityNotFoundException(typeof(User), updateUserRequest.Id);
                return validationResult;
            }

            var userFoundByEmail = await repository.FindByEmail(updateUserRequest.Email);
            var userWithEmailExists = userFoundByEmail != null;
            var isUserOwnerOfEmail = userFoundByEmail?.Id == updateUserRequest.Id;
            if (userWithEmailExists && !isUserOwnerOfEmail)
            {
                validationResult.Exception = new EntityAlreadyExistsException(typeof(User), nameof(User.Email), updateUserRequest.Email);
                return validationResult;
            }

            return validationResult;
        }
    }
}
