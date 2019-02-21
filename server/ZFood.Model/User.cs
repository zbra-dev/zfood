namespace ZFood.Model
{
    public class User
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string AvatarUrl { get; set; }

        public UserCredentials Credentials { get; set; }
    }
}
