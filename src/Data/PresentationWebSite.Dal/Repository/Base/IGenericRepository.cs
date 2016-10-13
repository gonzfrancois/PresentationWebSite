using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PresentationWebSite.Dal.Repository.Base
{
    public interface IGenericRepository<TEntity> where TEntity : class 
    {
        IEnumerable<TEntity> Get();

        TEntity Find(params object[] primaryKey);

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        TEntity Insert(TEntity entity);

        void DeleteAll(Expression<Func<TEntity, bool>> predicate);

        void Delete(int id);

        void Delete(TEntity entityToDelete);
        
        void Update(TEntity entityToUpdate);
    }
}