using System;

namespace ZFood.Core.API.Exceptions
{
    public class EntityNotFoundException : ValidationException
    {
        private readonly Type entityType;
        private readonly string entityId;

        public override string Message => $"Could not find {entityType.Name} {entityId}";

        public EntityNotFoundException(Type entityType, string entityId)
        {
            this.entityType = entityType;
            this.entityId = entityId;
        }
    }
}
