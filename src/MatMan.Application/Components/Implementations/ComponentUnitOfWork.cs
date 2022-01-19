using System;
using System.Collections.Generic;
using MatMan.Data;
using MatMan.Domain.Models;

namespace MatMan.Application
{
    public class ComponentUnitOfWork<TComponent, TComponentConfiguration> :
        UnitOfWorkBase,
        IComponentUnitOfWork<TComponent, TComponentConfiguration>
            where TComponent : Component, new()
            where TComponentConfiguration : Configuration, new()
    {
        protected readonly int _maxComponentNameLength = 30;

        protected readonly int _minComponentNameLength = 3;

        protected ComponentUnitOfWork(
            IRepository repo
        ) : base(repo) { }

        public IEnumerable<Unit> AllUnits =>
            _repository.Get<Unit>();

        protected TComponentConfiguration InitConfiguration(Guid componentID, Units unit)
        {
            var targetUnit = _repository.GetFirst<Unit>(u => u.FullName == GetUnitNameFrom(unit));

            return new TComponentConfiguration {
                ComponentID = componentID,
                UnitID = targetUnit.ID
            };
        }

        protected string GetUnitNameFrom(Units unit)
        {
            if (Enum.GetName(unit) == "Millimeter") return "миллиметр";
            if (Enum.GetName(unit) == "Centimeter") return "сантиметр";
            return "метр";
        }
    }
}
