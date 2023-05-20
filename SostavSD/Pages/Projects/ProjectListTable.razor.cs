using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using SostavSD.Interfaces;
using SostavSD.Models;
using SostavSD.Services;


namespace SostavSD.Pages.Projects
{
    partial class ProjectListTable
    {
        [Inject] IProjectService ProjectService { get; set; }
        private IDialogService _dialogService;

        private MudTable<ProjectForTableModel> mudTable;
        private IProjectForTableService _projectService;
        private IStringLocalizer<ProjectListTable> _localizer;
        private NavigationManager _navigationManager;

        private List<ProjectForTableModel> _projects;
        string searchString;
        string styleTableHeader = "font-size: 12px; text-align: center; padding: 0 0 0 10px; overflow-wrap: break-word; line-height: 1;";
		string styleTableBody = "padding: 0; text-align: center;";
    

		public ProjectListTable(IDialogService dialog, IProjectForTableService projectService, IStringLocalizer<ProjectListTable> localizer, NavigationManager navigation)
        {
            _projectService = projectService;
            _localizer = localizer;
            _navigationManager = navigation;
            _dialogService = dialog;
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
			|| ((project.Project.StageId > 0) && project.Project.DesignStage.StageName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
			|| (!string.IsNullOrWhiteSpace(project.Project.Contract.ContractNumber) && project.Project.Contract.ContractNumber.Contains(searchString, StringComparison.OrdinalIgnoreCase))
			|| (!string.IsNullOrWhiteSpace(project.Project.ProjectReleaseDate.ToString()) && project.Project.ProjectReleaseDate.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
			|| (!string.IsNullOrWhiteSpace(project.Project.ProjectReleaseDateByContract.ToString()) && project.Project.ProjectReleaseDateByContract.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
			|| (!string.IsNullOrWhiteSpace(project.Project.StatusDate.ToString()) && project.Project.StatusDate.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            || ((project.Project.StatusId > 0) && project.Project.Status.StatusName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            ;

			return result;
		}
        private void NavigateToTheNewProjectPage()
        {
            _navigationManager.NavigateTo("/projects/newproject");
        }
		private void RowClickEvent(TableRowClickEventArgs<ProjectForTableModel> tableRowClickEventArgs)
		{
            var parameters = new DialogParameters();

            var projectToEdit = ProjectService.GetProjectByIdAsync(tableRowClickEventArgs.Item.Project.ProjectId);
            
            parameters.Add("Project", projectToEdit);
            var dialog = _dialogService.Show<EditProject>("update", parameters).Result;
            if (dialog != null)
            {
               ProjectService.EditProjectAsync((ProjectModel)projectToEdit);
            }
        }

	}
}
