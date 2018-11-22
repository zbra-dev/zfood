using ZFood.Core.API;
using ZFood.Model;
using ZFood.Web.DTO;

namespace ZFood.Web.Extensions
{
    public static class RestaurantConversionExtensions
    {
        public static RestaurantDTO ToDTO(this Restaurant restaurant)
        {
            return new RestaurantDTO
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Address = restaurant.Address
            };
        }

        public static CreateRestaurantRequest FromDTO(this CreateRestaurantRequestDTO requestDTO)
        {
            return new CreateRestaurantRequest
            {
                Name = requestDTO.Name,
                Address = requestDTO.Address
            };
        }

        public static UpdateRestaurantRequest FromDTO(this UpdateRestaurantRequestDTO requestDTO, string id)
        {
            return new UpdateRestaurantRequest
            {
                Id = id,
                Name = requestDTO.Name,
                Address = requestDTO.Address
            };
        }
    }
}
