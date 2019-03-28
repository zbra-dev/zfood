namespace ZFood.Core.API
{
    public class PageRequest
    {
        public int Skip { get; set; }

        public int Take { get; set; }

        public string Query { get; set; }

        public bool Count { get; set; }
    }
}
