using System.Runtime.Serialization;

namespace ZFood.Web.DTO
{
    [DataContract(Name = "User")]
    public class UserDTO
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Email { get; set; }
    }
}
