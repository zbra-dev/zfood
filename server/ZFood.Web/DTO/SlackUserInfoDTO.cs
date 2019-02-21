using System.Runtime.Serialization;

namespace ZFood.Web.DTO
{
    [DataContract]
    public class SlackUserInfoDTO
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember(Name = "image_1024")]
        public string AvatarUrl { get; set; }
    }
}
