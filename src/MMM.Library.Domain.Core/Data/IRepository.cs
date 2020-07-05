using MMM.Library.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MMM.Library.Domain.Core.Data
{
    public interface IRepository<TEntity> 
        where TEntity : Entity
    {
        void Add(TEntity entityToAdd);
        void Update(TEntity entityToUpdate);
        void Delete(TEntity entityToRemove);
        void Delete(Guid id);

        Task<TEntity> GetById(Guid id);
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Find(Expression<Func<TEntity, bool>> predicate);


        Task<IEnumerable<TEntity>> GetList(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = "");
    }
}
