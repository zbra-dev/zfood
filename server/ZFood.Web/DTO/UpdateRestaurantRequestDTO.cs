using System.ComponentModel.DataAnnotations;

namespace ZFood.Web.DTO
{
    public class UpdateRestaurantRequestDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
