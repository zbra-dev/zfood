namespace ZFood.Core.API
{
    public class CreateUserRequest
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string AvatarUrl { get; set; }

        public string Provider { get; set; }

        public string ProviderId { get; set; }
    }
}
