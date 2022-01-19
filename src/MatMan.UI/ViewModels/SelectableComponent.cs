using System;
using MatMan.Domain.Models;

namespace MatMan.UI.ViewModels
{
    public class SelectableEntity<TEntity>
        where TEntity : Entity, new()
    {
        public Guid EntityID { get; init; }

        public TEntity Entity { get; init; }

        public bool IsSelected { get; init; }

        public double Amount { get; init; }
    }
}
