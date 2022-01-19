using System;
using System.Linq;
using System.Collections.Generic;
using MatMan.Domain.Models;

namespace MatMan.UI.ViewModels
{
    public class ConfigurableEntityViewModel<TEntity, TConfiguration> :
        CreatableEntityViewModel<TEntity>
            where TEntity : Entity, new()
            where TConfiguration : Configuration, new()
    {
        public TConfiguration NewEntityConfiguration { get; init; }

        public List<TConfiguration> EntitiesConfigurations { get; init; }

        public List<Unit> Units { get; init; }

        public TConfiguration GetEntityConfiguration(Guid entityID) =>
            EntitiesConfigurations.FirstOrDefault(mc => mc.ComponentID == entityID);
    }
}
