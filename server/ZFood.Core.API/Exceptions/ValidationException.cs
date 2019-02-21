using System;

namespace ZFood.Core.API.Exceptions
{
    public abstract class ValidationException : Exception
    {
        public override string Message { get; }

        protected ValidationException()
        {
        }

        protected ValidationException(string message)
        {
            Message = message;
        }
    }
}
