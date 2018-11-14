using System.ComponentModel.DataAnnotations;

namespace ZFood.Core.API
{
    public class CreateRestaurantRequestDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
