using SostavSD.Data;

namespace SostavSD.Interfaces
{
	public interface IEmailService
	{
		void Send(EmailMessage emailMessage, string path);
	}
}
