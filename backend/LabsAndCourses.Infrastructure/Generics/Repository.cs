﻿using System.Linq.Expressions;

namespace LabsAndCoursesManagement.Infrastructure.Generics
{
    public abstract class Repository<T>
        : IRepository<T> where T : class
    {
        protected DatabaseContext context;

        public Repository(DatabaseContext context)
        {
            this.context = context;
        }

        public virtual T Add(T entity)
        {
            return context
                .Add(entity)
                .Entity;
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>()
                .AsQueryable()
                .Where(predicate).ToList();
        }

        public virtual T Get(Guid id)
        {
            return context.Find<T>(id);
        }

        public virtual IEnumerable<T> All()
        {
            return context.Set<T>()
                .ToList();
        }

        public virtual T Update(T entity)
        {
            return context.Update(entity).Entity;
        }

        public virtual void Delete(Guid id) {
            T entry = context.Find<T>(id);
            context.Set<T>().Remove(entry);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
