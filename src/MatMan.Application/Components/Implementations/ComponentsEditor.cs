using System;
using MatMan.Data;
using MatMan.Domain.Models;

namespace MatMan.Application.Editors
{
    public class ComponentsEditor<TComponent, TComponentConfiguration> :
        ComponentUnitOfWork<TComponent, TComponentConfiguration>,
        IComponentsEditor<TComponent, TComponentConfiguration>
            where TComponent : Component, new()
            where TComponentConfiguration : Configuration, new()
    {
        public ComponentsEditor(
            IRepository repo
        ) : base(repo) { }

        /// <summary>
        /// Returns Guid of newly created component
        /// </summary>
        public Guid CreateComponent(TComponent newComponent, TComponentConfiguration newComponentConfig)
        {

            ValidateComponent(newComponent);
            ValidateComponentConfiguration(newComponentConfig);

            var componentCopy = new TComponent {
                Name = newComponent.Name.Trim()
            };

            _repository.Add(componentCopy);
            _repository.Save();

            var configCopy = new TComponentConfiguration {
                UnitID = newComponentConfig.UnitID,
                ComponentID = componentCopy.ID
            };

            _repository.Add(configCopy);
            _repository.Save();

            return componentCopy.ID;
        }

        /// <summary>
        /// Returns Guid of newly created component
        /// </summary>
        public Guid CreateComponent(TComponent newComponent)
        {
            ValidateComponent(newComponent);

            var componentCopy = new TComponent {
                Name = newComponent.Name.Trim()
            };

            _repository.Add(componentCopy);
            _repository.Save();

            return componentCopy.ID;
        }

        public void DeleteComponent(Guid componentID)
        {
            _repository.Remove<TComponent>(componentID);
            _repository.Save();
        }

        private void ValidateComponent(TComponent component)
        {
            var badEntityException = new BadEntityPropertyException(
                    message: $"Название компонента должно иметь длину от {_minComponentNameLength}" +
                        $" до {_maxComponentNameLength} символов",
                    entityName: $"{nameof(Component)}"
                );

            if (component.Name == null)
                throw badEntityException;

            if (component.Name.Length > _maxComponentNameLength && component.Name.Length < 3)
                throw badEntityException;

            if (_repository.GetFirst<TComponent>(m => m.Name == component.Name) != null)
                throw new EntityAlreadyExistsException(
                    $"Компонент с именем \"{component.Name}\" уже существует",
                    "Name"
                );
        }

        private void ValidateComponentConfiguration(TComponentConfiguration config)
        {
            var existingUnit = _repository.GetByPrimaryKey<Unit>(config.UnitID);
            if (existingUnit == null)
                throw new EntityDoesNotExistException(
                    message: $"There is no such {nameof(config.UnitID)} in database to add {nameof(TComponentConfiguration)}",
                    entityName: $"{nameof(Unit)}"
                );
        }

        private TComponentConfiguration InitComponentConfiguration(Guid componentID, Units unit)
        {
            var targetUnit = _repository.GetFirst<Unit>(u => u.FullName == GetUnitNameFrom(unit));

            return new TComponentConfiguration {
                ComponentID = componentID,
                UnitID = targetUnit.ID
            };
        }
    }
}
