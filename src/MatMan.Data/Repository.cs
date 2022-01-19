using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MatMan.Domain.Models;

namespace MatMan.Data
{
    public class Repository : IRepository
    {
        protected ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual TEntity GetByPrimaryKey<TEntity>(params object[] keys)
            where TEntity : Entity =>
            GetDbSet<TEntity>().Find(keys);

        public virtual TEntity GetFirst<TEntity>(Func<TEntity, bool> predicate)
            where TEntity : Entity =>
            GetDbSet<TEntity>().FirstOrDefault(predicate);

        public virtual IEnumerable<TEntity> Get<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = ""
        ) where TEntity : Entity
        {
            IQueryable<TEntity> query = GetDbSet<TEntity>();

            if (filter != null)
                query = query.Where(filter);

            var separators = new char[] { ',', ' ', '.', ':', ';', '|', '\\', '/', '_'};
            foreach (
                var propertyName in
                includeProperties.
                    Split(separators, StringSplitOptions.RemoveEmptyEntries)
            )
                query = query.Include(propertyName);

            if (orderBy != null)
                return orderBy(query).ToList();
            else
                return query.ToList();
        }

        public virtual void Add<TEntity>(TEntity entityToAdd)
            where TEntity : Entity =>
            GetDbSet<TEntity>().Add(entityToAdd);

        public virtual void Update<TEntity>(TEntity entityToUpdate)
            where TEntity : Entity
        {
            GetDbSet<TEntity>().Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void Remove<TEntity>(params object[] keys)
            where TEntity : Entity
        {
            var set = GetDbSet<TEntity>();

            var targetEntity = set.Find(keys);
            if (targetEntity == null) return;
            set.Remove(targetEntity);
        }

        public virtual void Remove<TEntity>(TEntity entityToRemove)
            where TEntity : Entity
        {
            var set = GetDbSet<TEntity>();

            if (_context.Entry(entityToRemove).State == EntityState.Detached)
                set.Attach(entityToRemove);
            set.Remove(entityToRemove);
        }

        public virtual void Save() =>
            _context.SaveChanges();

        private DbSet<TEntity> GetDbSet<TEntity>()
            where TEntity : Entity =>
            _context.Set<TEntity>();
    }
}
