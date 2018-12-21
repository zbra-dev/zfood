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
            if (!string.IsNullOrEmpty(query))
            {
                users = users.Where(u => u.Name.StartsWith(query));
            }
            return users.ToArrayAsync();
        }

        public async Task<UserEntity> CreateUser(UserEntity user)
        {
            var createdUser = await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return createdUser.Entity;
        }

        public async Task<int> GetTotalCount()
        {
            return await context.Users.CountAsync();
        }

        public async Task UpdateUser(UserEntity user)
        {
            var entry = context.Entry(user);
            entry.State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task DeleteUser(string id)
        {
            var entry = context.Entry(new UserEntity() { Id = id });
            entry.State = EntityState.Deleted;
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // TODO: Log error
            }
        }
    }
}
