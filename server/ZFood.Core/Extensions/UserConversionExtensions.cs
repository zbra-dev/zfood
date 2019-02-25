using System;
using ZFood.Core.API;
using ZFood.Model;
using ZFood.Persistence.API.Entity;

namespace ZFood.Core.Extensions
{
    public static class UserConversionExtensions
    {
        public static User ToModel(this UserEntity entity)
        {
            Enum.TryParse<CredentialsProvider>(entity.Provider, out var provider);
            return new User
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
                AvatarUrl = entity.AvatarUrl,
                Credentials = new UserCredentials
                {
                    Provider = provider,
                    ProviderId = entity.ProviderId,
                }
            };
        }

        public static UserEntity ToEntity(this CreateUserRequest request)
        {
            return new UserEntity
            {
                Id = null,
                Name = request.Name,
                Email = request.Email,
                Provider = request.Provider,
                ProviderId = request.ProviderId,
                AvatarUrl = request.AvatarUrl,
            };
        }

        public static UserEntity ToEntity(this UpdateUserRequest user)
        {
            return new UserEntity
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                AvatarUrl = user.AvatarUrl,
                Provider = user.Provider,
                ProviderId = user.ProviderId,
            };
        }
    }
}
