namespace ZFood.Persistence.API.Entity
{
    public class UserEntity
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Provider { get; set; }

        public string ProviderId { get; set; }

        public string AvatarUrl { get; set; }
    }
}
