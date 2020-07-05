using MMM.Library.Domain.Core.Models;
using System;
using System.Collections.Generic;

namespace MMM.Library.Domain.Models
{
    public class Publisher : Entity
    {
        public Publisher(string name, string phone, string address)
        {
            Name = name;
            Phone = phone;
        }

        public string Name { get; private set; }
        public string Phone { get; private set; }
        public string Address { get; private set; }

        // EF navigation properties
        public IEnumerable<Book> Books { get; set; }
        public Publisher() { }

        public static class PublisherFactory
        {
            public static Publisher NewPublisher(Guid id, string name, string phone, string address)
            {
                var publisher = new Publisher()
                {
                    Id = id,
                    Name = name,
                    Phone = phone,
            };
                return publisher;
            }
        }
    }
}
