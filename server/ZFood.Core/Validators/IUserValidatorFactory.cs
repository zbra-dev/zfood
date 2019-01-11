using ZFood.Core.API;

namespace ZFood.Core.Validators
{
    public interface IUserValidatorFactory
    {
        IValidator<CreateUserRequest> CreateUserCreationValidator();

        IValidator<UpdateUserRequest> CreateUpdateUserValidator();
    }
}
