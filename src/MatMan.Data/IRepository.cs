using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using MatMan.Domain.Models;

namespace MatMan.Data
{
    public interface IRepository
    {
        TEntity GetByPrimaryKey<TEntity>(params object[] keys)
            where TEntity : Entity;

        TEntity GetFirst<TEntity>(Func<TEntity, bool> predicate)
            where TEntity : Entity;

        IEnumerable<TEntity> Get<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = ""
        ) where TEntity : Entity;

        void Add<TEntity>(TEntity entityToAdd)
            where TEntity : Entity;

        void Update<TEntity>(TEntity entityToUpdate)
            where TEntity : Entity;

        void Remove<TEntity>(params object[] keys)
            where TEntity : Entity;

        void Remove<TEntity>(TEntity entityToRemove)
            where TEntity : Entity;

        void Save();
    }
}
