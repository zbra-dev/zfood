namespace ZFood.Model
{
    public class UserCredentials
    {
        public string Id { get; set; }

        public string ProviderId { get; set; }

        public CredentialsProvider Provider { get; set; }
    }
}
