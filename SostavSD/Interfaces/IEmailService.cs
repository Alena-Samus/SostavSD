using SostavSD.Classes.Email;

namespace SostavSD.Interfaces
{
    public interface IEmailService
	{
		void Send(EmailMessage emailMessage);
		Task<string> GetEmail();
		Task<string> GetUserName();

	}
}
