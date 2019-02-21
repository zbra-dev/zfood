using System;

namespace ZFood.Core.API.Exceptions
{
    public class InvalidValueException : ValidationException
    {
        public InvalidValueException(Type type, object value)
            : base($"{value} is not a valid {type.Name}")
        {
        }
    }
}
