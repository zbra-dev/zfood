using System.Linq;
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

        public async Task<User> FindById(string id)
        {
            var entity = await repository.FindById(id);
            return entity?.ToModel();
        }

        public async Task<User> FindByProviderId(CredentialsProvider provider, string providerId)
        {
            var entity = await repository.FindByProviderId(provider.ToString(), providerId);
            return entity?.ToModel();
        }

        public async Task<Page<User>> Get(int skip, int take, bool count, string query)
        {
            var increasedTake = take++;
            var userEntities = await repository.Get(skip, increasedTake, query);
            var users = userEntities.Select(u => u.ToModel()).ToArray();
            var hasMore = users.Length == increasedTake;
            int? totalCount = null;

            if (count)
            {
                totalCount = await repository.GetTotalCount();
            }

            return new Page<User>
            {
                Items = users,
                HasMore = hasMore,
                TotalCount = totalCount
            };
        }

        public async Task<User> CreateUser(CreateUserRequest userRequest)
        {
            var createdUser = await repository.CreateUser(userRequest.ToEntity());
            return createdUser.ToModel();
        }

        public async Task UpdateUser(UpdateUserRequest userRequest)
        {
            var user = await repository.FindById(userRequest.Id);
            userRequest.Provider = user.Provider;
            userRequest.ProviderId = user.ProviderId;
            userRequest.AvatarUrl = user.AvatarUrl;

            await repository.UpdateUser(userRequest.ToEntity());
        }

        public async Task DeleteUser(string id)
        {
            await repository.DeleteUser(id);
        }
    }
}
