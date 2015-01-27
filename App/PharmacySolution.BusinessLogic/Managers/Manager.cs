using System;
using System.Linq;
using System.Linq.Expressions;
using PharmacySolution.Contracts.Manager;
using PharmacySolution.Contracts.Repository;
using PharmacySolution.Contracts.Validator;

namespace PharmacySolution.BusinessLogic.Managers
{
    public class Manager<T> : IManager<T> where T : class 
    {
        private readonly IValidator<T> _validator = null;
        private readonly IRepository<T> _repository = null;

        public Manager(IValidator<T> validator, IRepository<T> repository)
        {
            _validator = validator;
            _repository = repository;
        }

        public void Add(T entity)
        {
            if (_validator.IsValid(entity))
            {
                _repository.Add(entity);
            }
        }

        public void Remove(T entity)
        {
             _repository.Remove(entity);
        }

        public bool Edit(T entity)
        {
            return _repository.Edit(entity);
        }

        public IQueryable<T> FindAll()
        {
            return _repository.FindAll();
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> preficate)
        {
            return _repository.Find(preficate);
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }

        public T GetByPrimaryKey(object key)
        {
            return _repository.GetByPrimaryKey(key);
        }
    }
}
