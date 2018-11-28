using ZFood.Model;
using System.Threading.Tasks;

namespace ZFood.Core.API
{
    public interface IUserService
    {
        Task<User> CreateUser(CreateUserRequest userRequest);

        Task UpdateUser(UpdateUserRequest user);
    }
}
