using System.Collections.Generic;
using MatMan.Domain.Models;

namespace MatMan.UI.ViewModels
{
    public class CreatableEntityViewModel<TEntity> :
        EntitiesListViewModel<TEntity>
            where TEntity : Entity, new()
    {
        public TEntity NewEntity { get; init; }
    }

    public class EntitiesListViewModel<TEntity>
        where TEntity : Entity, new()
    {
        public List<TEntity> Entities { get; init; }
    }
}
