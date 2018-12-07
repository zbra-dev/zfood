using System.Runtime.Serialization;

namespace ZFood.Web.DTO
{
    [DataContract(Name = "Restaurant")]
    public class RestaurantDTO
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Address { get; set; }
    }
}
