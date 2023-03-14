using MimeKit;
using SostavSD.Interfaces;
using MailKit.Net.Smtp;
using SostavSD.Classes.Email;
using System.Security.Claims;
using DocumentFormat.OpenXml.Spreadsheet;

namespace SostavSD.Services
{
    public class EmailService : IEmailService
	{
		private readonly IEmailConfiguration _emailConfiguration;

		private readonly IAuthorizedUserService _authorizedUserService;




		public EmailService(IEmailConfiguration emailConfiguration, IAuthorizedUserService authorizedUserService)
		{
			_emailConfiguration = emailConfiguration;
			_authorizedUserService = authorizedUserService;
		}
		public async Task<string> GetEmail()
		{
			var user = await _authorizedUserService.GetCurrentUserAsync();
			string _emailadress;
			_emailadress = user.FindFirst(c => c.Type == ClaimTypes.Email)?.Value.ToString();
			
			return _emailadress;

		}


		public void Send(EmailMessage emailMessage)
		{
			var message = new MimeMessage();

			//emailMessage.ToAddresses = GetEmail();
			message.To.Add(new MailboxAddress(emailMessage.ToAddresses.Name, emailMessage.ToAddresses.Address));
			message.From.Add(new MailboxAddress(emailMessage.FromAddresses.Name, emailMessage.FromAddresses.Address));
			//email.FromAddresses = new EmailAddress { Name = "Sostav", Address = "sostavsd@mail.ru" };
			//email.ToAddresses = new EmailAddress { Name = "", Address = "sostavsd@gmail.com" };

			//message.To.Add(new MailboxAddress("", "sostavsd@gmail.com"));
			//message.From.Add(new MailboxAddress("Sostav", "sostavsd@gmail.com"));

			message.Subject = emailMessage.Subject;
			var builder = new BodyBuilder();
			builder.TextBody = emailMessage.Content;
			builder.Attachments.Add(emailMessage.Attachment);
			message.Body = builder.ToMessageBody();
	

			using (var emailClient = new SmtpClient())
			{

				emailClient.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, MailKit.Security.SecureSocketOptions.Auto);

				emailClient.Authenticate(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);

				emailClient.Send(message);

				emailClient.Disconnect(true);

			}
		}


	}
}
