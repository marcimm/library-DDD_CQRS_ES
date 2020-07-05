using System;

namespace MMM.Library.Domain.Models
{
    public class BookAuthor
    {
        public Guid BookId { get; set; }
        public Book Book { get; set; }
        public Guid AuthorId { get; set; }
        public Author Author { get; set; }

        public BookAuthor(Book book, Author author)
        {
            Book = book;
            Author = author;
        }

        public BookAuthor(Guid bookId, Guid authorId)
        {
            BookId = bookId;
            AuthorId = authorId;
        }
        public BookAuthor() { }
    }
}
