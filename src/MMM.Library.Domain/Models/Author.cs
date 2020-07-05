using MMM.Library.Domain.Core.Models;
using System;
using System.Collections.Generic;

namespace MMM.Library.Domain.Models
{
    public class Author : Entity
    {
        public Author(string name, DateTime birthDate, string nationality)
        {
            Name = name;
            BirthDate = birthDate;
            Nationality = nationality;
        }

        public string Name { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string Nationality { get; private set; }

        // EF properties
        public IEnumerable<BookAuthor> BookAuthors { get; private set; }
        public Author() { }

        public int GetAge()
        {
            int age = DateTime.Now.Year - BirthDate.Year;

            if (DateTime.Today < BirthDate.AddYears(age)) age--;

            return age;
        }


        public static class AuthorFactory
        {
            public static Author NewAuthor(Guid id, string name, DateTime birthDate, string nationality)
            {
                var author = new Author()
                {
                    Id = id,
                    Name = name,
                    BirthDate = birthDate,
                    Nationality = nationality,
                };
                return author;
            }
        }
    }
}
