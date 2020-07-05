using MMM.Library.Domain.Core.Interfaces;
using MMM.Library.Domain.Core.Models;
using MMM.Library.Domain.Core.Validations;
using System;
using System.Collections.Generic;

namespace MMM.Library.Domain.Models
{
    public class Book : Entity, IAggregateRoot, IAudit
    {
        public Book(Guid categoryId, Guid publisherId, string isbn, string title, int year, string language)
        {
            CategoryId = categoryId;
            PublisherId = publisherId;
            ISBN = isbn;
            Title = title;
            Year = year;
            Language = language;
            BookAuthors = new HashSet<BookAuthor>();

            //Audit = new AuditEntity();

            Validate();
        }

        public Guid CategoryId { get; private set; }
        public Guid PublisherId { get; private set; }
        public string ISBN { get; private set; }
        public string Title { get; private set; }
        public int Year { get; private set; }
        public string Language { get; private set; }

        public string CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public string LastUpdateUser { get; set; }
        public DateTime? LastUpdateDate { get; set; }



        // public AuditEntity Audit { get; private set; }

        // EF navigation properties
        public Book() { }
        public Category Category { get; private set; }
        public Publisher Publisher { get; private set; }
        public HashSet<BookAuthor> BookAuthors { get; private set; }
        public IEnumerable<Stock> Stocks { get; private set; }
        
        public void UpdateBook(Guid categoryId, Guid publisherId, string title, int year, string language)
        {
            CategoryId = categoryId;
            PublisherId = publisherId;
            Title = title;
            Year = year;
            Language = language;
        }

        public void UpdateAuthors(ICollection<Guid> authorIds)
        {
            RemoveAuthors();
            foreach (Guid authorId in authorIds)
            {
                BookAuthors.Add(new BookAuthor(this.Id, authorId));
            }
        }

        public void RemoveAuthors()
        {
            BookAuthors?.Clear();
        }

        public void Validate()
        {
            AssertionConcern.ValidateIfEmpty(Title, "Título não pode ser nulo!");
            AssertionConcern.ValidateIfEmpty(ISBN, "ISBN não pode ser nulo!");
            AssertionConcern.ValidateIfEqual(CategoryId, Guid.Empty, "Id da categoria do livro não pode ser nulo");
            AssertionConcern.ValidateIfEqual(PublisherId, Guid.Empty, "Id da editora do livro não pode ser nulo");
            AssertionConcern.ValidateMinMax(Year, 1500, DateTime.Today.Year, "O ano do livro de ser entre 1500 e " + DateTime.Today.Year);
        }

        // 
        public static class BookFactory
        {
            public static Book NewBook(Guid id, Guid categoryId, Guid publisherId, string isbn, string title, int year, string language)

            {
                var book = new Book()
                {
                    Id = id,
                    CategoryId = categoryId,
                    PublisherId = publisherId,
                    ISBN = isbn,
                    Title = title,
                    Year = year,
                    Language = language
                };

                return book;
            }
        }

    }
}
