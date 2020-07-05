using Microsoft.EntityFrameworkCore;
using MMM.Library.Domain.Core.Data;
using MMM.Library.Domain.Core.Models;
using MMM.Library.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MMM.Library.Infra.Data.Repository
{
    public class GenericRepository<TEntity> : IRepository<TEntity>
     where TEntity : Entity, new()
    {
        protected readonly LibraryDbContext _dbContext;

        public GenericRepository(LibraryDbContext context)
        {
            _dbContext = context;
        }

        public void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }
        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }

        public void Delete(Guid id)
        {
            _dbContext.Set<TEntity>()
                .Remove(_dbContext.Set<TEntity>().Find(id));
        }
        public void Delete(TEntity entity)
        {
            // var entity = _dbContext.Set<TEntity>().Find(id);
             _dbContext.Set<TEntity>().Remove(entity);
        }

        // OPTIONS TO SOFT Delete ---
        public void DeleteSoft(Guid id)
        {
            var entity = _dbContext.Set<TEntity>().Find(id);
            entity.MarkAsDelete();
            Update(entity);
        }       

        public virtual async Task<TEntity> GetById(Guid id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }
        public virtual async Task<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().AsNoTracking().Where(predicate).FirstOrDefaultAsync();
        }        
        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        // --->  IQueryable<T>
        public virtual async Task<IEnumerable<TEntity>> GetList(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>()
                .AsNoTracking(); //* Disable tracking 

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public virtual async Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.FirstOrDefaultAsync();
        }
        // <--
               
    }
}
