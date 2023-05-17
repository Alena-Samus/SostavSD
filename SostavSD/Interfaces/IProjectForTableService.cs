using SostavSD.Models;

namespace SostavSD.Interfaces
{
	public interface IProjectForTableService
	{
		Task<List<ProjectForTableModel>> GetProjectsAsync();
	}
}
