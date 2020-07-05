using MMM.Library.Domain.Core.Data;
using MMM.Library.Domain.Core.Models;
using MMM.Library.Domain.CQRS.Interfaces;
using MMM.Library.Domain.Models;
using System;
using System.Threading.Tasks;

namespace MMM.Library.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit();

        // Generic Repositories ==> CRUD      
        IRepository<Category> CategoryRepository { get; }
        IRepository<Author> AuthorRepository { get; }
        IRepository<Publisher> PublisherRepository { get; }

        // Extends to specialized repository
        IBookRepository BookRepository { get; }
        IBookingRepository BookingRepository { get; }


        // Alternative Options: Generics with reflection
        IRepository<TEntity> GetBaseRepository<TEntity>()
          where TEntity : Entity, new();

    }
}