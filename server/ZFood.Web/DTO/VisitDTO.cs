using System.Runtime.Serialization;

namespace ZFood.Web.DTO
{
    [DataContract(Name = "Visit")]
    public class VisitDTO
    {
        public string Id { get; set; }

        public int Rate { get; set; }

        public string RestaurantId { get; set; }

        public RestaurantDTO Restaurant { get; set; }

        public string UserId { get; set; }

        public UserDTO User { get; set; }
    }
}
