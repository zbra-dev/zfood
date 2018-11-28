using System;
using System.Threading.Tasks;
using ZFood.Core.API;
using ZFood.Core.API.Exceptions;
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

        public async Task<User> FindById(string id)
        {
            var entity = await repository.FindById(id);
            return entity?.ToModel();
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

        public async Task UpdateUser(UpdateUserRequest userRequest)
        {
            if (userRequest == null)
            {
                throw new ArgumentNullException(nameof(userRequest));
            }

            var user = await FindById(userRequest.Id);

            if (user == null)
            {
                throw new EntityNotFoundException($"Could not find User {userRequest.Id}");
            }

            await repository.UpdateUser(userRequest.ToEntity());
        }
    }
}
