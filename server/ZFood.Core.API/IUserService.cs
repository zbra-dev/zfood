using System.Threading.Tasks;
using ZFood.Model;

namespace ZFood.Core.API
{
    public interface IUserService
    {
        Task<User> FindById(string id);

        Task<User> FindByProviderId(CredentialsProvider provider, string providerId);

        Task<Page<User>> Get(PageRequest pageRequest);

        Task<User> CreateUser(CreateUserRequest userRequest);

        Task UpdateUser(UpdateUserRequest user);

        Task DeleteUser(string id);
    }
}
