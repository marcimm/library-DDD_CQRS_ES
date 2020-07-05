using System.Collections.Generic;

namespace MMM.Library.Application.ViewModels
{
    public class EmailMessageViewModel
    {
        public EmailMessageViewModel(IEnumerable<string> mailToList, string mailFrom, string subject, string body, bool isBodyHtml)
        {
            MailFrom = mailFrom;
            Subject = subject;
            Body = body;
            IsBodyHtml = isBodyHtml;
            MailToList = mailToList;
        }

        public string MailFrom { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsBodyHtml { get; set; }
        public IEnumerable<string> MailToList { get; protected set; }
    }
}
