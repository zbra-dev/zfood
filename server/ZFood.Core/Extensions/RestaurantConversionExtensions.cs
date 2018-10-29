using ZFood.Model;
using ZFood.Persistence.API.Entity;

namespace ZFood.Core.Extensions
{
    public static class RestaurantConversionExtensions
    {
        public static Restaurant ToModel(this RestaurantEntity entity)
        {
            return new Restaurant
            {
                Id = entity.Id,
                Name = entity.Name,
                Address = entity.Address
            };
        }

        public static RestaurantEntity ToEntity(this Restaurant restaurant)
        {
            return new RestaurantEntity
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Address = restaurant.Address
            };
        }
    }
}
