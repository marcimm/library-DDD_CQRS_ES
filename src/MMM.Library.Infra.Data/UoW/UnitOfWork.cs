using MMM.Library.Domain.Core.Data;
using MMM.Library.Domain.Core.Models;
using MMM.Library.Domain.CQRS.Interfaces;
using MMM.Library.Domain.Interfaces;
using MMM.Library.Domain.Models;
using MMM.Library.Infra.Data.Context;
using MMM.Library.Infra.Data.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MMM.Library.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly LibraryDbContext _dbContext;

        public UnitOfWork(LibraryDbContext context)
        {
            _dbContext = context;
        }

        public async Task<bool> Commit()
        {
            return await _dbContext.Commit();
        }       

        // Generic Repositories ==> CRUD   
        private GenericRepository<Category> categoryRepository;
        private GenericRepository<Publisher> publisherRepository;
        private GenericRepository<Author> authorRepository;      
        public IRepository<Category> CategoryRepository
        {
            get
            {
                if (categoryRepository == null)
                {
                    categoryRepository = new GenericRepository<Category>(_dbContext);
                }
                return categoryRepository;
            }
        }
        public IRepository<Publisher> PublisherRepository
        {
            get
            {
                if (publisherRepository == null)
                {
                    publisherRepository = new GenericRepository<Publisher>(_dbContext);
                }
                return publisherRepository;
            }
        }
        public IRepository<Author> AuthorRepository
        {
            get
            {
                if (authorRepository == null)
                {
                    authorRepository = new GenericRepository<Author>(_dbContext);
                }
                return authorRepository;
            }
        }


        // Extends to specialized repository
        private IBookingRepository bookingRepository;
        private IBookRepository bookRepository;
        public IBookingRepository BookingRepository
        {
            get
            {
                if (bookRepository == null)
                {
                    bookingRepository = new BookingRepository(_dbContext);
                }
                return bookingRepository;
            }
        }
        public IBookRepository BookRepository
        {
            get
            {
                if (bookRepository == null)
                {
                    bookRepository = new BookRepository(_dbContext);
                }
                return bookRepository;
            }
        }


        // --> Alternative Options Generics with reflection
        private Dictionary<Type, object> _repositories;
        public IRepository<TEntity> GetBaseRepository<TEntity>()
          where TEntity : Entity, new()
        {
            if (_repositories == null) _repositories = new Dictionary<Type, object>();

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new GenericRepository<TEntity>(_dbContext);
            }

            return (IRepository<TEntity>)_repositories[type];
        }
        // <--
       


        #region disposable
        private bool _disposedValue = false; // To detect redundant calls 
        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;

            if (disposing)
            {
                _dbContext?.Dispose();
            }
            _disposedValue = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
