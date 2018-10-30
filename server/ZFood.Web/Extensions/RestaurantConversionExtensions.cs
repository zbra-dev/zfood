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
    }
}
