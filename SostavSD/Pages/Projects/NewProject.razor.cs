using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using SostavSD.Interfaces;
using SostavSD.Models;
using System.Runtime.CompilerServices;

namespace SostavSD.Pages.Projects
{
	partial class NewProject
	{
		private NavigationManager _navigationManager;
		
		[Inject] IStringLocalizer<NewProject> Localizer { get; set; }
		[Inject] IContractForTableService ContractService { get; set; }
		[Inject] IBuildingViewService BuildingViewService { get; set; }
		[Inject] IDesignStageService DesignStageService { get; set; }
		[Inject] IProjectService ProjectService { get; set; }
		[Inject] ISnackbar Snackbar { get; set; }

	

		private List<ContractForTableModel> _contracts = new();
		private List<BuildingViewModel> _viewes = new();
		private List<DesignStageModel> _stages = new();

		private ContractForTableModel _selectedContract = new();
		
		private BuildingViewModel _selectedBuildingView = new();

		private DesignStageModel _selectedDesignStage = new();

		private string _toProject = "/projects";
		private string _toContracts = "/contracts";

	

		private ProjectModel _newProject = new();

		protected override async Task OnInitializedAsync()
		{			
			_newProject = new ProjectModel();
		}
		public NewProject(NavigationManager navigationManager)
		{
			_navigationManager= navigationManager;

		}
		private void GoToPage(string adress)
		{
			_navigationManager.NavigateTo(adress);
		}

		protected async Task<IEnumerable<ContractForTableModel>> FindContract(string value)
		{
			_contracts = await ContractService.GetContractsAsync();

			if (string.IsNullOrEmpty(value))
			{
				return _contracts;
			}
			else
			{
				return _contracts.Where(x => x.Contract.Index.Contains(value, StringComparison.InvariantCultureIgnoreCase));
			}

		}
		protected async Task<IEnumerable<BuildingViewModel>> FindBuildingView(string value)
		{
			_viewes = await BuildingViewService.GetAllBuildingView();

			if (string.IsNullOrEmpty(value))
			{
				return _viewes;
			}
			else
			{
				return _viewes.Where(x => x.BuildingViewName.Contains(value, StringComparison.InvariantCultureIgnoreCase));
			}

		}
		protected async Task<IEnumerable<DesignStageModel>> FindDesignStage(string value)
		{
			_stages = await DesignStageService.GetAllDesignStageAsync();

			if (string.IsNullOrEmpty(value))
			{
				return _stages;
			}
			else
			{
				return _stages.Where(x => x.StageName.Contains(value, StringComparison.InvariantCultureIgnoreCase));
			}

		}

		private async Task Save()
		{
			_newProject.ContractId = _selectedContract.Contract.ContractID;
			_newProject.BuildingViewId = _selectedBuildingView.BuildingViewId;
			_newProject.StageId = _selectedDesignStage.StageId;

			if (await ProjectService.AddProjectAsync(_newProject))
			{
				Snackbar.Add("Add", Severity.Success);
			}
			else
			{
				Snackbar.Add("Don't add", Severity.Error);
			}			
			
			GoToPage(_toProject);
		}

	}
}
