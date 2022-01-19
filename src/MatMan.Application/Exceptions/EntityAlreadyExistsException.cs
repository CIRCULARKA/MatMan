using System;

namespace MatMan.Application
{
    public class EntityAlreadyExistsException : Exception
    {
        public EntityAlreadyExistsException(
            string message, string propertyName) : base(message)
        {
            IssuedProperty = propertyName;
        }

        public string IssuedProperty { get; init; }
    }
}
