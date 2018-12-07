using ZFood.Model;
using System.Threading.Tasks;

namespace ZFood.Core.API
{
    public interface IUserService
    {
        Task<User> FindById(string id);

        Task<Page<User>> Get(int skip, int take, bool count, string query);

        Task<User> CreateUser(CreateUserRequest userRequest);

        Task UpdateUser(UpdateUserRequest user);

        Task DeleteUser(string id);
    }
}
