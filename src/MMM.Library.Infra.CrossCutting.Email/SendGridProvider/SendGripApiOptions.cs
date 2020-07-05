namespace MMM.Library.Infra.CrossCutting.Email.SendGridProvider
{
    public class SendGripApiOptions
    {
        public string SendGridUser { get; set; }
        public string SendGridKey { get; set; }
        public string EmailSender { get; set; }
    }
}
