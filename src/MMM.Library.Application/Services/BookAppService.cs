using AutoMapper;
using MMM.Library.Application.Interfaces;
using MMM.Library.Application.ViewModels;
using MMM.Library.Domain.Core.Mediator;
using MMM.Library.Domain.Core.Notifications;
using MMM.Library.Domain.Interfaces;
using MMM.Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MMM.Library.Application.Services
{
    public class BookAppService : IBookAppService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediatorHandler _mediatorHandler;

        public BookAppService(IMapper mapper, IUnitOfWork unitOfWork,
                              IMediatorHandler mediatorHandler)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _mediatorHandler = mediatorHandler;
        }

        // Regra de Negócio - N.1: Verificar título existente
        private async Task<bool> CheckIfBookExists(BookWriteViewModel bookWriteViewModel)
        {
            var book = await _unitOfWork.BookRepository.GetSingle(b => b.Title == bookWriteViewModel.Title && b.Id != bookWriteViewModel.Id,
                includeProperties: "BookAuthors");

            if (book != null)
            {
                await _mediatorHandler.PublishNotification(new Notification("Violação de Regra", "Livro com mesmo título já cadastrado!"));
                return true;
            }
            return false;
        }

        public async Task<BookWriteViewModel> AddNewBook(BookWriteViewModel bookWriteViewModel)
        {
            if (await CheckIfBookExists(bookWriteViewModel)) return null;

            var book = _mapper.Map<Book>(bookWriteViewModel);

            book.UpdateAuthors(bookWriteViewModel.AuthorsId);

            //_unitOfWork.GetBaseRepository<Book>().Add(book); // Options Reflections on repository base
            _unitOfWork.BookRepository.Add(book);

            if (!await _unitOfWork.Commit())
            {
                await _mediatorHandler.PublishNotification(new Notification("Erro", "Erro ao gravar dados!"));
                return null;
            }

            return _mapper.Map<BookWriteViewModel>(book);
        }

        public async Task<bool> UpdateBook(BookWriteViewModel bookWriteViewModel)
        {
            var book = await _unitOfWork.BookRepository.GetSingle(b => b.Id == bookWriteViewModel.Id, includeProperties: "BookAuthors");

            if (book == null)
            {
                await _mediatorHandler.PublishNotification(new Notification("Erro", "Livro Não encontrado!"));
                return false;
            }

            book.UpdateBook(bookWriteViewModel.CategoryId, bookWriteViewModel.PublisherId, bookWriteViewModel.Title,
                bookWriteViewModel.Year, bookWriteViewModel.Language);

            book.UpdateAuthors(bookWriteViewModel.AuthorsId);
            _unitOfWork.BookRepository.Add(book);
            _unitOfWork.BookRepository.Update(book);

            if (!await _unitOfWork.Commit())
            {
                await _mediatorHandler.PublishNotification(new Notification("Erro", "Erro ao gravar dados!"));
                return false;
            }

            return true;
        }

        public async Task<BookViewModel> DeleteBook(Guid id)
        {
            var book = await _unitOfWork.BookRepository.GetSingle(b => b.Id == id, includeProperties: "BookAuthors");

            if (book == null) return null;

            book.RemoveAuthors();
            _unitOfWork.BookRepository.Update(book);
            _unitOfWork.BookRepository.Delete(book.Id);

            if (!await _unitOfWork.Commit())
            {
                await _mediatorHandler.PublishNotification(new Notification("Erro", "Erro ao gravar dados!"));
                return null;
            }

            return _mapper.Map<BookViewModel>(book);
        }

        public async Task<BookViewModel> GetById(Guid id)
        {
            return _mapper.Map<BookViewModel>(await _unitOfWork.BookRepository.GetSingle(x => x.Id == id,
                includeProperties: "Publisher,Category"));
        }

        public async Task<IEnumerable<BookViewModel>> GetAllBooksWithAllData()
        {
            return _mapper.Map<IEnumerable<BookViewModel>>(await _unitOfWork.BookRepository.GetBooksWithAllData());
        }

    }
}
