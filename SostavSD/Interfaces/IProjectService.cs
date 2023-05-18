using SostavSD.Models;

namespace SostavSD.Interfaces
{
	public interface IProjectService
	{
		Task <List<ProjectModel>> GetProjectsAsync ();
		Task <bool> AddProjectAsync(ProjectModel newProject);
	}
}
