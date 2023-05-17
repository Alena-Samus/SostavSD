using Microsoft.Extensions.Localization;
using SostavSD.Interfaces;
using SostavSD.Models;
using SostavSD.Pages.Contracts;

namespace SostavSD.Pages.Projects
{
    partial class ProjectListTable
    {
        private IProjectForTableService _projectService;
        private IStringLocalizer<ProjectListTable> _localizer;

        private List<ProjectForTableModel> _projects;
        string searchString;
        string styleTableHeader = "font-size: 12px; text-align: center; padding: 0 0 0 10px; overflow-wrap: break-word; line-height: 1;";
		string styleTableBody = "padding: 0; text-align: center;";
    

		public ProjectListTable(IProjectForTableService projectService, IStringLocalizer<ProjectListTable> localizer)
        {
            _projectService = projectService;
            _localizer = localizer;
        }

        protected override async Task OnInitializedAsync()
        {
            _projects = await _projectService.GetProjectsAsync();
        }

        private bool FilterFuncCurrent(ProjectForTableModel project) => FilterFunc(project, searchString);
        private bool FilterFunc(ProjectForTableModel project, string searchString)
        {
			bool result = string.IsNullOrWhiteSpace(searchString)
			|| ((project.Project.Priority > 1) && project.Project.Priority.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
			|| (!string.IsNullOrWhiteSpace(project.Project.Contract.Index) && project.Project.Contract.Index.Contains(searchString, StringComparison.OrdinalIgnoreCase))
			|| (!string.IsNullOrWhiteSpace(project.Project.BuildingNumber) && project.Project.BuildingNumber.Contains(searchString, StringComparison.OrdinalIgnoreCase))
			|| (!string.IsNullOrWhiteSpace(project.Project.ProjectName) && project.Project.ProjectName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
			|| (!string.IsNullOrWhiteSpace(project.Project.Contract.UserID) && project.Project.Contract.Executor.Surname.Contains(searchString, StringComparison.OrdinalIgnoreCase))
			|| (!string.IsNullOrWhiteSpace(project.Calculator.UserSurname) && project.Calculator.UserSurname.Contains(searchString, StringComparison.OrdinalIgnoreCase))
			|| (!string.IsNullOrWhiteSpace(project.Project.DesignStage.ToString()) && project.Project.DesignStage.StageName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
			|| (!string.IsNullOrWhiteSpace(project.Project.Contract.ContractNumber) && project.Project.Contract.ContractNumber.Contains(searchString, StringComparison.OrdinalIgnoreCase))
			|| (!string.IsNullOrWhiteSpace(project.Project.ProjectReleaseDate.ToString()) && project.Project.ProjectReleaseDate.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
			|| (!string.IsNullOrWhiteSpace(project.Project.ProjectReleaseDateByContract.ToString()) && project.Project.ProjectReleaseDateByContract.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
			|| (!string.IsNullOrWhiteSpace(project.Project.StatusDate.ToString()) && project.Project.StatusDate.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
			|| ((project.Project.StatusId > 1) && project.Project.Status.StatusName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
			;

			return result;
		}
    }
}
