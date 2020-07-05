using System;

namespace MMM.Library.Application.ViewModels
{
    public class AuditViewModel
    {
        public string CreateUserName { get; set; }
        public string LastUpdateUserName { get; set; }

        public string CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public string LastUpdateUser { get; set; }
        public DateTime? LastUpdateDate { get; set; }
    }
}
