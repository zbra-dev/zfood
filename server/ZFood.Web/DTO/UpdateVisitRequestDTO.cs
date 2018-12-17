using System.ComponentModel.DataAnnotations;

namespace ZFood.Web.DTO
{
    public class UpdateVisitRequestDTO
    {
        [Required]
        public int? Rate { get; set; }

        [Required]
        public string RestaurantId { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
