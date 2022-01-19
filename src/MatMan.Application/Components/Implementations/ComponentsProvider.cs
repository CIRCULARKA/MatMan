using System;
using System.Collections.Generic;
using MatMan.Domain.Models;
using MatMan.Data;

namespace MatMan.Application.Providers
{
    public class ComponentsProvider<TComponent, TComponentConfiguration> :
        ComponentUnitOfWork<TComponent, TComponentConfiguration>,
        IComponentsProvider<TComponent, TComponentConfiguration>
            where TComponent : Component, new()
            where TComponentConfiguration : Configuration, new()
    {
        public ComponentsProvider(
            IRepository repo
        ) : base(repo) { }

        public IEnumerable<TComponent> AllComponents =>
            _repository.Get<TComponent>();

        public IEnumerable<TComponentConfiguration> AllComponentsConfigurations =>
            _repository.Get<TComponentConfiguration>(includeProperties: nameof(Unit));

        public TComponent GetComponentByID(Guid componentID) =>
            _repository.GetByPrimaryKey<TComponent>(componentID);

        public TComponent GetComponentByName(string name) =>
            _repository.GetFirst<TComponent>(c => c.Name == name);

        public TComponentConfiguration GetComponentConfiguration(Guid componentID) =>
            _repository.GetFirst<TComponentConfiguration>(m => m.ComponentID == componentID);
    }
}
