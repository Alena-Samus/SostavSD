using SostavSD.Interfaces;
using SostavSD.Models;
using SostavSD.Shared;

namespace SostavSD.Services
{
	public class ProjectForTableService : IProjectForTableService
	{
		private readonly IProjectService _projectService;
		private readonly IAuthorizedUserService _authorizedUserService;

		public ProjectForTableService(IProjectService projectService, IAuthorizedUserService authorizedUserService)
		{
			_projectService = projectService;
			_authorizedUserService = authorizedUserService;
		}
				
		public async Task<List<ProjectForTableModel>> GetProjectsAsync()
		{
			List <ProjectModel> _projects = await _projectService.GetProjectsAsync();

			List<ProjectForTableModel> _projectsForTable = new();

			foreach (var project in _projects)
			{
				ManagerUserModel currentCalculator = new ManagerUserModel();

				if (project.Contract.CalculatorId is not null)
				{
					currentCalculator = await _authorizedUserService.GetSingleUser(project.Contract.CalculatorId);
				}

				var current = new ProjectForTableModel 
				{
					Project= project,
					Calculator = currentCalculator,
                };

				_projectsForTable.Add(current);
			}

			return _projectsForTable;
		}
	}
}
