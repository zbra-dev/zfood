using System;
using System.Text;

namespace ZFood.Core.API.Exceptions
{
    public class EntityAlreadyExistsException : ValidationException
    {
        public override string Message { get; }

        public EntityAlreadyExistsException(Type entityType, string propertyName, object propertyValue)
        {
            Message = $"{entityType.Name}'s {propertyName} {propertyValue} already exists";
        }

        public EntityAlreadyExistsException(Type entityType, params EntityValuePair[] entityValuePairs)
        {
            var messageBuilder = new StringBuilder();
            foreach (var parameter in entityValuePairs)
            {
                messageBuilder.Append($"{parameter.PropertyName}: {parameter.PropertyValue}, ");
            }
            Message = $"{entityType.Name} with {messageBuilder.ToString()}already exists";
        }
    }
}
