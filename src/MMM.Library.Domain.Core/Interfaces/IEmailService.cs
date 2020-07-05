using System.Collections.Generic;
using System.Threading.Tasks;

namespace MMM.Library.Domain.Core.Interfaces
{
    public interface IEmailService
    {
        // TODO-P: refatorar retorno email
        Task<string> SendEmailAsync(IEnumerable<string> mailToList, string mailFrom,
            string subject, string body, bool isBodyHtml);
    }
}
