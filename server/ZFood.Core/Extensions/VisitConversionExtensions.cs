using ZFood.Model;
using ZFood.Persistence.API.Entity;

namespace ZFood.Core.Extensions
{
    public static class VisitConversionExtensions
    {
        public static Visit ToModel(this VisitEntity entity)
        {
            return new Visit
            {
                Id = entity.Id,
                Rate = entity.Rate,
                RestaurantId = entity.RestaurantId,
                Restaurant = entity.Restaurant.ToModel(),
                UserId = entity.UserId,
                User = entity.User.ToModel()
            };
        }
    }
}
