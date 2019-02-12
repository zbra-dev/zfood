using System;

namespace ZFood.Core.API.Exceptions
{
    public class EntityAlreadyExistsException : ValidationException
    {
        private readonly Type entityType;
        private readonly string entityId;

        public override string Message => $"{entityType.Name} {entityId} already exists.";

        public EntityAlreadyExistsException(Type entityType, string entityId)
        {
            this.entityType = entityType;
            this.entityId = entityId;
        }
    }
}
