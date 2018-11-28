using Microsoft.EntityFrameworkCore;
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

        public async Task<UserEntity> CreateUser(UserEntity user)
        {
            var createdUser = await context.Users.AddAsync(user);
            context.SaveChanges();
            return createdUser.Entity;
        }

        public async Task UpdateUser(UserEntity user)
        {
            var userToBeUpdated = await FindById(user.Id);
            userToBeUpdated = user;
            context.Users.Update(userToBeUpdated);
            context.SaveChanges();
        }
    }
}
