using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using SostavSD.Interfaces;
using SostavSD.Models;
namespace SostavSD.Pages.Projects
{
    partial class ProjectListTable
    {
        [Inject] IEntityManagementService EntityManagementService { get; set; }
		[Inject] ISnackbar Snackbar { get; set; }
		private IDialogService _dialogService;

       
        private IProjectForTableService _projectService;
        private IStringLocalizer<ProjectListTable> _localizer;
        private NavigationManager _navigationManager;

        private List<ProjectForTableModel> _projects;
		private HashSet<ProjectForTableModel> selectedItems = new HashSet<ProjectForTableModel>();

        private string _toNewProject = "/projects/newproject";
		private string _toContracts = "/contracts";


		private string searchString;

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
        private async Task<List<ProjectForTableModel>> GetProjects()
        {
            _projects.Clear();
			_projects = await _projectService.GetProjectsAsync();
            return _projects;
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
        private void NavigateToPage(string adress)
        {           
			_navigationManager.NavigateTo(adress);
		}
		
        private void ItemHasBeenComitted(object item)
        {            
            EntityManagementService.EditProjectAsync(((ProjectForTableModel)item).Project);  
		}

        private async Task  RemoveProjects()
        {
            if (selectedItems.Count != 0)
            {
				foreach (var project in selectedItems)
				{
					await EntityManagementService.DeleteProjectAsync(project.Project.ProjectId);
				}
				Snackbar.Add(_localizer["itemsRemoved"], Severity.Success);
                await GetProjects();
			}
            else
            {
                Snackbar.Add(_localizer["noItems"], Severity.Info);
            }

        }

		private async Task CopyProject()
		{
			if (selectedItems.Count == 0)
			{
				Snackbar.Add(_localizer["noItems"], Severity.Info);
			}
			if (selectedItems.Count == 1)
			{				
				var currentProject = selectedItems.ElementAt(0);
				var parameters = new DialogParameters();
				parameters.Add("BuildingNumber", string.Empty);

				var dialog = await _dialogService.Show<CopyProject>("Copy", parameters).Result;

				if (dialog.Data != null)
				{
					ProjectModel newProject = new ProjectModel
					{
						BuildingNumber = (string)dialog.Data,
						ContractId = currentProject.Project.ContractId,
						ConstructionPhase = currentProject.Project.ConstructionPhase,
						StageId = currentProject.Project.StageId,
						BuildingViewId = currentProject.Project.BuildingViewId,
						ProjectReleaseDate = currentProject.Project.ProjectReleaseDate,
						ProjectReleaseDateByContract = currentProject.Project.ProjectReleaseDateByContract,
						WorkStartDate = currentProject.Project.WorkStartDate,
						PriceLevel = currentProject.Project.PriceLevel,
						PrintType = currentProject.Project.PrintType,
						CiCVersion = currentProject.Project.CiCVersion,
						ProjectName = currentProject.Project.ProjectName,
					};
					
					await EntityManagementService.AddProjectAsync(newProject);
					await GetProjects();
					
					Snackbar.Add(_localizer["copied"], Severity.Success);
				}		
				
			}
			if (selectedItems.Count > 1)
			{
				Snackbar.Add(_localizer["someItems"], Severity.Info);
			}
			selectedItems.Clear();
		}

		private async Task OpenEditDialog(TableRowClickEventArgs<ProjectForTableModel> tableRowClickEventArgs)
        {
            var currentProject = tableRowClickEventArgs.Item.Project;
            if ( await EntityManagementService.EditProjectAsync(currentProject))
            {
                Snackbar.Add(_localizer["projectEdited"], Severity.Success);
                await GetProjects();
            }
            else
            {
                Snackbar.Add(_localizer["projectNotEdited"], Severity.Error);
            }			

		}

	}
}
