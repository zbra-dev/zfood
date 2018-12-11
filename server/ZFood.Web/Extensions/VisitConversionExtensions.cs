using ZFood.Core.API;
using ZFood.Model;
using ZFood.Web.DTO;

namespace ZFood.Web.Extensions
{
    public static class VisitConversionExtensions
    {
        public static VisitDTO ToDTO(this Visit visit)
        {
            return new VisitDTO
            {
                Id = visit.Id,
                Rate = visit.Rate,
                Restaurant = visit.Restaurant.ToDTO(),
                User = visit.User.ToDTO()
            };
        }

        public static CreateVisitRequest FromDTO (this CreateVisitRequestDTO dto)
        {
            return new CreateVisitRequest
            {
                Rate = dto.Rate,
                RestaurantId = dto.RestaurantId,
                UserId = dto.UserId
            };
        }
    }
}
