namespace ZFood.Model
{
    public class Visit
    {
        public string Id { get; set; }

        public int Rate { get; set; }

        public string RestaurantId { get; set; }

        public Restaurant Restaurant { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
