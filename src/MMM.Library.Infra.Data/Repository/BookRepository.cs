using Microsoft.EntityFrameworkCore;
using MMM.Library.Domain.Interfaces;
using MMM.Library.Domain.Models;
using MMM.Library.Infra.Data.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMM.Library.Infra.Data.Repository
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(LibraryDbContext context) : base(context)
        { }

        public async Task<IEnumerable<Book>> GetBooksWithAllData()
        {
            var book = await _dbContext.Books.AsNoTracking()
                .Include(p => p.BookAuthors).ThenInclude(p => p.Author)
                .Include(p => p.Category).Include(p => p.Publisher)
                .OrderByDescending(p => p.CreateDate).ToListAsync();

            return book;
        }
    }
}
