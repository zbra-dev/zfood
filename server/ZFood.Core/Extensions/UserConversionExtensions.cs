using ZFood.Core.API;
using ZFood.Model;
using ZFood.Persistence.API.Entity;

namespace ZFood.Core.Extensions
{
    public static class UserConversionExtensions
    {

        public static User ToModel(this UserEntity entity)
        {
            return new User
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
                Username = entity.Username
            };
        }

        public static UserEntity ToEntity(this CreateUserRequest user)
        {
            return new UserEntity
            {
                Id = null,
                Name = user.Name,
                Email = user.Email,
                Username = user.Username
            };
        }

        public static UserEntity ToEntity(this UpdateUserRequest user)
        {
            return new UserEntity
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Username = user.Username
            };
        }
    }
}
