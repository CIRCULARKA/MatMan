using System;

namespace MatMan.Application
{
    public class EntityDoesNotExistException : Exception
    {

        public EntityDoesNotExistException(string message,
            string entityName = "") : base(message) =>
            EntityName = entityName;

        public string EntityName { get; init; }
    }
}
