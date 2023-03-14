using MimeKit.Text;
using MimeKit;
using SostavSD.Data;
using SostavSD.Interfaces;
using MailKit.Net.Smtp;

namespace SostavSD.Services
{
	public class EmailService : IEmailService
	{
		private readonly IEmailConfiguration _emailConfiguration;

		public EmailService(IEmailConfiguration emailConfiguration)
		{
			_emailConfiguration = emailConfiguration;

		}


		public void Send(EmailMessage emailMessage)
		{
			var message = new MimeMessage();
			message.To.AddRange(emailMessage.ToAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
			message.From.AddRange(emailMessage.FromAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));

			message.Subject = emailMessage.Subject;
			var builder = new BodyBuilder();
			builder.TextBody = emailMessage.Content;
			builder.Attachments.Add(emailMessage.Attachment);
			message.Body = builder.ToMessageBody();
	

			using (var emailClient = new SmtpClient())
			{

				emailClient.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, true);

				emailClient.Authenticate(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);

				emailClient.Send(message);

				emailClient.Disconnect(true);

			}
		}
	}
}
