using System;

namespace MMM.Library.Domain.Core.Interfaces
{
    public interface IAudit
    {
        public string CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public string LastUpdateUser { get; set; }
        public DateTime? LastUpdateDate { get; set; }

    }
}
