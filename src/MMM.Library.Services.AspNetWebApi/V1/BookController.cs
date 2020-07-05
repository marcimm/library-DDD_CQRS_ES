using MediatR;
using Microsoft.AspNetCore.Mvc;
using MMM.Library.Application.Interfaces;
using MMM.Library.Application.ViewModels;
using MMM.Library.Domain.Core.Mediator;
using MMM.Library.Domain.Core.Notifications;
using MMM.Library.Domain.CQRS.Queries;
using MMM.Library.Domain.CQRS.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MMM.Library.Services.AspNetWebApi.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/book")]
    public class BookController : ApiBaseController
    {

        private readonly IBookAppService _bookAppService;
        private readonly IBookQueries _bookQueries;
        public BookController(INotificationHandler<Notification> notifications,
                              IMediatorHandler mediatorHandler,
                              IBookAppService bookAppService,
                              IBookQueries bookQueries)
            : base(notifications, mediatorHandler)
        {
            _bookAppService = bookAppService;
            _bookQueries = bookQueries;
        }

        [HttpGet]
        [Route("get-book-with-all-data")]
        public async Task<IEnumerable<BookViewModel>> GetBookWithAllData()
        {
            // Exemplo Queries CQRS sem AutoMapper
            return await _bookAppService.GetAllBooksWithAllData();
        }

        //[ClaimsAuthorize("Book", "Add")]
        [HttpPost]
        [Route("add-new")]
        public async Task<ActionResult<BookViewModel>> AddNewBook(BookWriteViewModel bookWriteViewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var bookRegistered = await _bookAppService.AddNewBook(bookWriteViewModel);

            return CustomResponse(bookRegistered);
        }

        [HttpPut]
        [Route("update/{id:guid}")]
        public async Task<IActionResult> UpdateBook(Guid id, BookWriteViewModel bookViewModel)
        {
            if (id != bookViewModel.Id)
            {
                NotifyError("Id Inválido", "Erro: Id fornecido não é válido");
                return CustomResponse();
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _bookAppService.UpdateBook(bookViewModel);

            return CustomResponse(bookViewModel);
        }

        [HttpDelete]
        [Route("delete/{id:guid}")]
        public async Task<ActionResult<BookViewModel>> DeleteBook(Guid id)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

           return CustomResponse(await _bookAppService.DeleteBook(id));
        }
       

        // ***************************************************************************** 

        //[ClaimsAuthorize("Book", "Read")]
        //[Authorize(Policy = "IsDeveloper")]
        [HttpGet]
        [Route("get-all")]
        public async Task<IEnumerable<BookAndCategoryViewModel>> GetAll()
        {
            // Exemplo Queries CQRS sem AutoMapper
            return await _bookQueries.GetAllBooksWithCategory();
        }    
    }
}
