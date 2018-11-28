using System.Threading.Tasks;
using ZFood.Core.API;
using ZFood.Core.Extensions;
using ZFood.Model;
using ZFood.Persistence.API;

namespace ZFood.Core
{
    public class UserService : IUserService
    {
        private readonly IUserRepository repository;

        public UserService(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<User> CreateUser(CreateUserRequest userRequest)
        {
            if (userRequest == null)
            {
                throw new System.ArgumentNullException(nameof(userRequest));
            }

            var createdUser = await repository.CreateUser(userRequest.ToEntity());
            return createdUser.ToModel();
        }

    }
}
