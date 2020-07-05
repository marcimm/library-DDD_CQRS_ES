using MMM.Library.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MMM.Library.Application.Interfaces
{
    public interface IBookAppService
    {
        // CRUD
        Task<BookViewModel> GetById(Guid id);
        Task<IEnumerable<BookViewModel>> GetAllBooksWithAllData();
        Task<BookWriteViewModel> AddNewBook(BookWriteViewModel bookViewModel);
        Task<bool> UpdateBook(BookWriteViewModel bookViewModel);
        Task<BookViewModel> DeleteBook(Guid id);
    }
}
