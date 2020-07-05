using MMM.Library.Domain.Core.Models;
using System;

namespace MMM.Library.Domain.Models
{
    public class Stock : Entity
    {
        public Stock(Guid bookId, int edition, int code)
        {
            BookId = bookId;
            Edition = edition;
            Code = code;
        }

        public Guid BookId { get; private set; }
        public int Edition { get; private set; }
        public int Code { get; private set; }

        public AuditEntity Audit { get; private set; }

        // EF
        public Stock() { }
        public Book Book { get; private set; }

    }
}
