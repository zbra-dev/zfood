namespace ZFood.Core.API.Exceptions
{
    public class LessThanException : ValidationException
    {
        public LessThanException(int minimumExpectedValue)
            : base($"Expected a value equals to or greater than {minimumExpectedValue}")
        {
        }
    }
}
