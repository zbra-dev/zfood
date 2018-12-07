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
    }
}
