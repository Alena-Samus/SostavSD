using SostavSD.Models;
using System.Threading.Tasks;

namespace SostavSD.Interfaces
{
	public interface IProjectService
	{
		Task<List<ProjectModel>> GetProjectsAsync();
		Task<bool> AddProjectAsync(ProjectModel newProject);
		Task<bool> EditProjectAsync(ProjectModel newProject);
		Task<bool> DeleteProjectAsync(int id);
		Task<ProjectModel> GetProjectByIdAsync(int id);
		bool CheckBuildingNumber(string buildingNumber);

		Task<int> GetPtojectIdAsync(string buildingNumber);
		Task<bool> UpdateCiCVersionAsync(int projectId, string newCiCVersion);
        Task<bool> UpdateCoefficientsAsync(int projectId, double? newPK1, double? newPK2);
    }
}
