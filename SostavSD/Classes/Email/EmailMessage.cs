﻿using System.Net.Mail;

namespace SostavSD.Classes.Email
{
    public class EmailMessage
    {
        public EmailMessage()
        {
            //ToAddresses = new List<EmailAddress>();
            //FromAddresses = new List<EmailAddress>();
        }

		//public List<EmailAddress> ToAddresses { get; set; }
		//public List<EmailAddress> FromAddresses { get; set; }
		public EmailAddress ToAddresses { get; set; }
		public EmailAddress FromAddresses { get; set; }
		public string Subject { get; set; }
        public string Content { get; set; }
        public string Attachment { get; set; }
    }
}
