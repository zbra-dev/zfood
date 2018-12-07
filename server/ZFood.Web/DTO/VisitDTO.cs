using System.Runtime.Serialization;

namespace ZFood.Web.DTO
{
    [DataContract(Name = "Visit")]
    public class VisitDTO
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public int Rate { get; set; }

        [DataMember]
        public RestaurantDTO Restaurant { get; set; }

        [DataMember]
        public UserDTO User { get; set; }
    }
}
