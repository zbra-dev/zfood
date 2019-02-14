namespace ZFood.Core.API.Exceptions
{
    public class EntityValuePair
    {
        public string PropertyName { get; }

        public object PropertyValue { get; }

        public EntityValuePair(string propertyName, object propertyValue)
        {
            PropertyName = propertyName;
            PropertyValue = propertyValue;
        }
    }
}
