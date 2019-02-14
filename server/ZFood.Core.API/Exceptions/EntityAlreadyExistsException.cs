using System;

namespace ZFood.Core.API.Exceptions
{
    public class EntityAlreadyExistsException : ValidationException
    {
        public override string Message { get; }

        public EntityAlreadyExistsException(Type entityType, string propertyName, object propertyValue)
        {
            Message = $"{entityType.Name}'s {propertyName} {propertyValue} already exists";
        }
    }
}
