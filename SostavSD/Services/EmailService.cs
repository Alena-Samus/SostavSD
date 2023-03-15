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

		public async Task<string> GetUserName()
		{
			var user = await _authorizedUserService.GetCurrentUserAsync();
			string _userName;
			_userName = user.FindFirst(c => c.Type == ClaimTypes.Name)?.Value.ToString();

			return _userName;
		}

		public void Send(EmailMessage emailMessage)
		{
			var message = new MimeMessage();
		

			message.From.Add(new MailboxAddress(_emailConfiguration.SmtpUsername, _emailConfiguration.EmailAddress));
			message.To.Add(new MailboxAddress(emailMessage.ToAddress.Name, emailMessage.ToAddress.Address));


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
