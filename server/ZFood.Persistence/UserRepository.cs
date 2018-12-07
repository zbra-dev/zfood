using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using ZFood.Persistence.API;
using ZFood.Persistence.API.Entity;

namespace ZFood.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly ZFoodDbContext context;

        public UserRepository(ZFoodDbContext context)
        {
            this.context = context;
        }

        public async Task<UserEntity> FindById(string id)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public Task<UserEntity[]> Get(int skip, int take, string query)
        {
            var users = context.Users.Skip(skip).Take(take);
            if(!string.IsNullOrEmpty(query))
            {
                users = users.Where(u => u.Name.StartsWith(query));
            }
            return users.ToArrayAsync();
        }

        public async Task<UserEntity> CreateUser(UserEntity user)
        {
            var createdUser = await context.Users.AddAsync(user);
            context.SaveChanges();
            return createdUser.Entity;
        }

        public async Task<int> GetTotalCount()
        {
            return await context.Users.CountAsync();
        }

        public async Task UpdateUser(UserEntity user)
        {
            var userToBeUpdated = await FindById(user.Id);
            userToBeUpdated = user;
            context.Users.Update(userToBeUpdated);
            context.SaveChanges();
        }

        public async Task DeleteUser(string id)
        {
            // TODO: Avoid hitting database twice
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user != null)
            {
                context.Users.Remove(user);
                context.SaveChanges();
            }
        }
    }
}
