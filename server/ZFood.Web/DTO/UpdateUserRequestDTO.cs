using System.ComponentModel.DataAnnotations;

namespace ZFood.Web.DTO
{
    public class UpdateUserRequestDTO
    {
        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }
    }
}