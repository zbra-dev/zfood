using System;

namespace ZFood.Core.API.Exceptions
{
    public class EntityAlreadyExistsException : ValidationException
    {
        private readonly Type entityType;
        private readonly string propertyName;
        private readonly string propertyValue;

        public override string Message { get; }

        public EntityAlreadyExistsException(Type entityType, string propertyName, string propertyValue)
        {
            this.entityType = entityType;
            this.propertyName = propertyName;
            this.propertyValue = propertyValue;

            Message = $"{entityType.Name}'s {propertyName} {propertyValue} already exists";
        }
    }
}
