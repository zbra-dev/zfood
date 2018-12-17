namespace ZFood.Core.API
{
    public class UpdateVisitRequest
    {
        public string Id { get; set; }

        public int Rate { get; set; }

        public string RestaurantId { get; set; }

        public string UserId { get; set; }
    }
}
