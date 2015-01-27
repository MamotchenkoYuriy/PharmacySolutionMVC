using System;
using System.Linq;
using System.Linq.Expressions;

namespace PharmacySolution.Contracts.Repository
{
    public interface IRepository<T> where T : class 
    {
        void Add(T entity);
        void Remove(T entity);
        bool Edit(T entity);
        IQueryable<T> FindAll();
        IQueryable<T> Find(Expression<Func<T, bool>> preficate );
        void SaveChanges();
        T GetByPrimaryKey(object key);
    }
}
