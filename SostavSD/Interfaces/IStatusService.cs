using SostavSD.Models;

namespace SostavSD.Interfaces
{
	public interface IStatusService
	{
		Task <List<StatusModel>> GetAllStatusAsync ();
	}
}
