using System.ComponentModel.DataAnnotations;

namespace MMM.Library.Infra.CrossCutting.Email.SmtpClientEmail
{
    public class SmtpConfiguration
    {
        public string From { get; set; }
        public string SmtpServer { get; set; }        
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
    }
}
