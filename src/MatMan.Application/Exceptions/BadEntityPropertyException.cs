using System;

namespace MatMan.Application
{
    public class BadEntityPropertyException : Exception
    {

        public BadEntityPropertyException(string message,
            string entityName = "", string propertyName = "") : base(message)
        {
            EntityName = entityName;
            PropertyName = propertyName;
        }

        public string EntityName { get; init; }

        public string PropertyName { get; init; }
    }
}
