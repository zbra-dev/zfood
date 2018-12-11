namespace ZFood.Core.API
{
    public class CreateVisitRequest
    {
        public int Rate { get; set; }

        public string RestaurantId { get; set; }

        public string UserId { get; set; }
    }
}
