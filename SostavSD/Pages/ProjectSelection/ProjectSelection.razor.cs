using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using SostavSD.Interfaces;
using SostavSD.Models;
using SostavSD.Services;

namespace SostavSD.Pages.ProjectSelection
{
    partial class ProjectSelection
    {
        [Inject] public IEntityManagementService EntityManagementService { get; set; }
		[Inject] public IAuthorizedUserService AuthorizedUserService { get; set; }
		[Inject] public IStringLocalizer<ProjectSelection> Localizer { get; set; }

        List<ManagerUserModel> Calculators = new();

        private List<ProjectModel> _projects = new();

		private List<UsersForListModel> _usersCPE = new List<UsersForListModel>();
		private List<DesignStageModel> _designStages = new List<DesignStageModel>();

		string findIndex;
        string findCPE;
		string findBuildingNumber;
		string findContractNumber;
		int? findDesignStage;

		protected override async Task OnInitializedAsync()
        {
            await GetProjectsAsync();
			_usersCPE = AuthorizedUserService.GetListUserSostavModelByGroup("8");
			_designStages = await EntityManagementService.GetAllDesignStageAsync();
			Calculators = await AuthorizedUserService.GetAllUsersAsync();

		}

        private async Task <List<ProjectModel>> GetProjectsAsync()
        {
            _projects = await EntityManagementService.GetProjectsAsync();
            return _projects;
        }

        private void Find()
        {
            List<ProjectModel> tempProjects = new(_projects);
            List<ProjectModel> _selectedProjects = new();

			if (!string.IsNullOrWhiteSpace(findCPE))
			{
				_selectedProjects = tempProjects.Where(x => x.Contract.Executor.Id == findCPE).ToList();
                tempProjects= _selectedProjects;
			}

			if (!string.IsNullOrWhiteSpace(findIndex))
            {
                _selectedProjects = tempProjects.Where(x => x.Contract.Index == findIndex).ToList();
                tempProjects= _selectedProjects;
			}

			if (!string.IsNullOrWhiteSpace(findBuildingNumber))
			{
				_selectedProjects = tempProjects.Where(x => x.BuildingNumber == findBuildingNumber).ToList();
				tempProjects = _selectedProjects;
			}
			if (!string.IsNullOrWhiteSpace(findContractNumber))
			{
				_selectedProjects = tempProjects.Where(x => x.Contract.ContractNumber.Contains(findContractNumber)).ToList();
				tempProjects = _selectedProjects;
			}
			if (findDesignStage != null)
			{
				_selectedProjects = tempProjects.Where(x => x.StageId == findDesignStage).ToList();
				tempProjects = _selectedProjects;
			}
			_projects.Clear();
			foreach (var _project in _selectedProjects)
			{
				_projects.Add(_project);
			}
			StateHasChanged();
		}

        private async Task Clean()
        {
            findIndex= null;
            findCPE= null;
            findBuildingNumber= null;
			findContractNumber= null;
			findDesignStage = null;
			await GetProjectsAsync();
        }
    }
}
