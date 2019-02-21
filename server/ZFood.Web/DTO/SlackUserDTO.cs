using System.Runtime.Serialization;

namespace ZFood.Web.DTO
{
    [DataContract]
    public class SlackUserDTO
    {
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }

        [DataMember(Name = "user")]
        public SlackUserInfoDTO UserInfo { get; set; }
    }
}
