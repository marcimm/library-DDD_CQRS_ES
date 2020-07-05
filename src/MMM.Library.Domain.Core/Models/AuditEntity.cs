using System;
using System.Collections.Generic;

namespace MMM.Library.Domain.Core.Models
{
    public class AuditEntity : ValueObject
    {

        public string CreateUser { get; private set; }
        public DateTime? CreateDate { get; private set; }
        public string LastUpdateUser { get; private set; }
        public DateTime? LastUpdateDate { get; private set; }

        public AuditEntity() { }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return CreateUser;
            yield return CreateDate;
            yield return LastUpdateUser;
            yield return LastUpdateDate;
        }
    }
}
