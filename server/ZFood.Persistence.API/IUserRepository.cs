using System.Threading.Tasks;
using ZFood.Persistence.API.Entity;

namespace ZFood.Persistence.API
{
    public interface IUserRepository
    {
        Task<UserEntity> FindById(string id);

        Task<UserEntity[]> Get(int skip, int take, string query);

        Task<int> GetTotalCount();

        Task<UserEntity> CreateUser(UserEntity user);

        Task UpdateUser(UserEntity user);

        Task DeleteUser(string id);
    }
}
