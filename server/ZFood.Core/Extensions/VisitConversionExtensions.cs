using ZFood.Core.API;
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
                Restaurant = entity.Restaurant.ToModel(),
                User = entity.User.ToModel()
            };
        }

        public static VisitEntity ToEntity(this CreateVisitRequest visitRequest)
        {
            return new VisitEntity
            {
                Id = null,
                Rate = visitRequest.Rate,
                RestaurantId = visitRequest.RestaurantId,
                Restaurant = null,
                UserId = visitRequest.UserId,
                User = null,
            };
        }

        public static VisitEntity ToEntity(this UpdateVisitRequest visitRequest)
        {
            return new VisitEntity
            {
                Id = visitRequest.Id,
                Rate = visitRequest.Rate,
                RestaurantId = visitRequest.RestaurantId,
                Restaurant = null,
                UserId = visitRequest.UserId,
                User = null,
            };
        }
    }
}
