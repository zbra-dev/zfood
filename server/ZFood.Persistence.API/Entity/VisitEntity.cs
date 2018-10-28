namespace ZFood.Persistence.API.Entity
{
    public class VisitEntity
    {
        public string Id { get; set; }

        public int Rate { get; set; }

        public string RestaurantId { get; set; }

        public RestaurantEntity Restaurant { get; set; }

        public string UserId { get; set; }

        public UserEntity User { get; set; }
    }
}
