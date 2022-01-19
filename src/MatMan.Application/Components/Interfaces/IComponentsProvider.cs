using System;
using System.Collections.Generic;
using MatMan.Domain.Models;

namespace MatMan.Application.Providers
{
    public interface IComponentsProvider<TComponent, TComponentConfiguration> :
        IComponentUnitOfWork<TComponent, TComponentConfiguration>
            where TComponent : Component, new()
            where TComponentConfiguration : Configuration, new()
    {
        IEnumerable<TComponent> AllComponents { get; }

        IEnumerable<TComponentConfiguration> AllComponentsConfigurations { get; }

        TComponent GetComponentByID(Guid componentID);

        TComponent GetComponentByName(string name);

        TComponentConfiguration GetComponentConfiguration(Guid componentID);
    }
}
