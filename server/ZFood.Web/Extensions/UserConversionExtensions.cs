using ZFood.Core.API;
using ZFood.Model;
using ZFood.Web.DTO;

namespace ZFood.Web.Extensions
{
    public static class UserConversionExtensions
    {
        public static UserDTO ToDTO(this User user)
        {
            return new UserDTO
            {
                Name = user.Name,
                Email = user.Email,
                AvatarUrl = user.AvatarUrl,
            };
        }

        public static CreateUserRequest FromDTO(this CreateUserRequestDTO requestDTO)
        {
            return new CreateUserRequest
            {
                Name = requestDTO.Name,
                Email = requestDTO.Email,
                Provider = requestDTO.Provider,
                ProviderId = requestDTO.ProviderId,
            };
        }

        public static UpdateUserRequest FromDTO(this UpdateUserRequestDTO requestDTO, string id)
        {
            return new UpdateUserRequest
            {
                Id = id,
                Name = requestDTO.Name,
                Email = requestDTO.Email,
            };
        }
    }
}
