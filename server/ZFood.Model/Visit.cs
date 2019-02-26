namespace ZFood.Model
{
    public class Visit
    {
        public string Id { get; set; }

        public int Rate { get; set; }

        public Restaurant Restaurant { get; set; }

        public User User { get; set; }
    }
}
