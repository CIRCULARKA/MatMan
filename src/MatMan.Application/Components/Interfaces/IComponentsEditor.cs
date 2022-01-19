using System;
using MatMan.Domain.Models;

namespace MatMan.Application.Editors
{
    public interface IComponentsEditor<TComponent, TComponentConfiguration> :
        IComponentUnitOfWork<TComponent, TComponentConfiguration>
            where TComponent : Component, new()
            where TComponentConfiguration : Configuration, new()
    {
        Guid CreateComponent(TComponent newComponent, TComponentConfiguration componentConfiguration);

        Guid CreateComponent(TComponent newComponent);

        void DeleteComponent(Guid componentID);
    }
}
