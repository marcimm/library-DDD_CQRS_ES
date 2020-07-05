using AutoMapper;
using MMM.Library.Application.ViewModels;
using MMM.Library.Domain.Core.EvetSourcing;
using MMM.Library.Domain.Core.Models;
using MMM.Library.Domain.Models;
using System;
using System.Linq;

namespace MMM.Library.Application.AutoMapper
{
    public class AutoMapperSetup : Profile
    {
        public AutoMapperSetup()
        {
            // Domain <-> ViewModel ------------------------------------------------------
            CreateMap<AuditEntity, AuditViewModel>().ReverseMap();
            CreateMap<Author, AuthorViewModel>().ReverseMap();
            CreateMap<BookAuthor, BookAuthorViewModel>().ReverseMap();
            CreateMap<Category, CategoryViewModel>().ReverseMap();
            CreateMap<Publisher, PublisherViewModel>().ReverseMap();
            CreateMap<StoredEvent, StoredEventViewModel>().ReverseMap();

            CreateMap<EmailMessage, EmailMessageViewModel>().ReverseMap();

            // Domain -> ViewModel ---------------------------------------------------------
            CreateMap<Book, BookWriteViewModel>().ForMember(p => p.AuthorsId, opt => opt.MapFrom(p => p.BookAuthors.Select(x => x.AuthorId)));
            CreateMap<Book, BookViewModel>().ForMember(p => p.Authors, opt => opt.MapFrom(p => p.BookAuthors.Select(x => x.Author)));


            // ViewModel -> Domain---------------------------------------------------------
            CreateMap<BookWriteViewModel, Book>()
                 .ForMember(p => p.CategoryId, opt => opt.MapFrom(p => p.CategoryId))
                 .ForMember(p => p.PublisherId, opt => opt.MapFrom(p => p.PublisherId))
                 .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => ((Guid)srcMember) != Guid.Empty));

            CreateMap<BookViewModel, Book>()
                .ForMember(p => p.CategoryId, opt => opt.MapFrom(p => p.Category.Id))
                .ForMember(p => p.PublisherId, opt => opt.MapFrom(p => p.Publisher.Id));
        }
    }
}
