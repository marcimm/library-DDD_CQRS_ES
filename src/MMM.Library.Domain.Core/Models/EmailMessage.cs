using System.Collections.Generic;

namespace MMM.Library.Domain.Core.Models
{
    public class EmailMessage
    {
        public EmailMessage(IEnumerable<string> mailToList, string mailFrom, string subject, string body, bool isBodyHtml)
        {
            MailToList = mailToList;
            MailFrom = mailFrom;
            Subject = subject;
            Body = body;
            IsBodyHtml = isBodyHtml;           
        }

        public string MailFrom { get; protected set; }
        public string Subject { get; protected set; }
        public string Body { get; protected set; }
        public bool IsBodyHtml { get; protected set; }
        public IEnumerable<string> MailToList { get; protected set; }


        
    }
}
