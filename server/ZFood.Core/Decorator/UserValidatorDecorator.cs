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

        public UserValidatorDecorator(IUserService userService, IUserValidatorFactory userValidatorFactory)
        {
            this.userService = userService;
            this.userValidatorFactory = userValidatorFactory;
        }

        public async Task<User> CreateUser(CreateUserRequest userRequest)
        {
            await userValidatorFactory.CreateUserCreationValidator().ThrowIfNotValid(userRequest);
            return await userService.CreateUser(userRequest);
        }

        public async Task DeleteUser(string id)
        {
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

        public async Task<Page<User>> Get(int skip, int take, bool count, string query)
        {
            return await userService.Get(skip, take, count, query);
        }

        public async Task UpdateUser(UpdateUserRequest user)
        {
            await userValidatorFactory.CreateUpdateUserValidator().ThrowIfNotValid(user);
            await userService.UpdateUser(user);
        }
    }
}
