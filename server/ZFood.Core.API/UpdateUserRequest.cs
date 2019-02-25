namespace ZFood.Core.API
{
    public class UpdateUserRequest
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string AvatarUrl { get; set; }

        public string Provider { get; set; }

        public string ProviderId { get; set; }
    }
}