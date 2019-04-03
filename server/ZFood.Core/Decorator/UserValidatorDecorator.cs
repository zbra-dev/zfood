using System.Threading.Tasks;
using ZFood.Core.API;
using ZFood.Core.Utils;
using ZFood.Core.Validators;
using ZFood.Model;

namespace ZFood.Core.Decorator
{
    public class UserValidatorDecorator : IUserService
    {
        private readonly IUserService userService;
        private readonly IUserValidatorFactory userValidatorFactory;
        private readonly IPageRequestValidatorFactory pageRequestValidatorFactory;

        public UserValidatorDecorator(IUserService userService, 
            IUserValidatorFactory userValidatorFactory,
            IPageRequestValidatorFactory pageRequestValidatorFactory)
        {
            this.userService = userService;
            this.userValidatorFactory = userValidatorFactory;
            this.pageRequestValidatorFactory = pageRequestValidatorFactory;
        }

        public async Task<User> CreateUser(CreateUserRequest userRequest)
        {
            await userValidatorFactory.CreateUserCreationValidator().ThrowIfNotValid(userRequest);
            return await userService.CreateUser(userRequest);
        }

        public async Task DeleteUser(string id)
        {
            await userValidatorFactory.CreateDeleteUserValidator().ThrowIfNotValid(id);
            await userService.DeleteUser(id);
        }

        public async Task<User> FindById(string id)
        {
            return await userService.FindById(id);
        }

        public Task<User> FindByProviderId(CredentialsProvider provider, string providerId)
        {
            return userService.FindByProviderId(provider, providerId);
        }

        public async Task<Page<User>> Get(PageRequest pageRequest)
        {
            await pageRequestValidatorFactory.CreatePageRequestValidator().ThrowIfNotValid(pageRequest);
            return await userService.Get(pageRequest);
        }

        public async Task UpdateUser(UpdateUserRequest user)
        {
            await userValidatorFactory.CreateUpdateUserValidator().ThrowIfNotValid(user);
            await userService.UpdateUser(user);
        }
    }
}
