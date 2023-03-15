using System.Net.Mail;

namespace SostavSD.Classes.Email
{
    public class EmailMessage
    {  
		public EmailAddress ToAddress { get; set; }
		public EmailAddress FromAddress { get; set; }
		public string Subject { get; set; }
        public string Content { get; set; }
        public string Attachment { get; set; }
    }
}
