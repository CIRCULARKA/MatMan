using System;
using System.Collections.Generic;
using MatMan.Domain.Models;

namespace MatMan.Application
{
    public interface IComponentUnitOfWork<TComponent, TComponentConfiguration>
        where TComponent : Component, new()
        where TComponentConfiguration : Configuration, new()
    {
        IEnumerable<Unit> AllUnits { get; }
    }
}
