namespace ZFood.Core.API.Exceptions
{
    public class AuthenticationException : ValidationException
    {
        public AuthenticationException(string message)
            : base(message)
        {
        }
    }
}
