using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using MudBlazor;
using SostavSD.Areas.Identity.Constants;
using SostavSD.Classes.Validation;
using SostavSD.Interfaces;
using SostavSD.Models;
using SostavSD.Pages.Contracts;
using SostavSD.Services;
using SostavSD.Shared;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace SostavSD.Pages.Projects
{
	partial class NewProject
	{
		private NavigationManager _navigationManager;


		[Inject] IProjectService ProjectService { get; set; }
		[Inject] IContractForTableService ContractForTableService { get; set; }
		[Inject] IBuildingViewService BuildingViewService { get; set; }
		[Inject] IDesignStageService DesignStageService { get; set; }
		[Inject] IStringLocalizer<NewProject> Localizer { get; set; }
		[Inject] IAuthorizedUserService AuthorizedUserService { get; set; }
		[Inject] ISnackbar Snackbar { get; set; }
		[Inject] IEditService EntityManagementService { get; set; }

	

		private List<ContractForTableModel> _contracts = new();
		private List<BuildingViewModel> _viewes = new();
		private List<DesignStageModel> _stages = new();
		private List<UsersForListModel> _mainDepWorkers = new();
		

		private ContractForTableModel _selectedContract = new();
		
		private BuildingViewModel _selectedBuildingView = new();

		private DesignStageModel _selectedDesignStage = new();
        private UsersForListModel _selectedMainWorker = new();

        private ProjectModelValidation _projectModelValidation = new();


        private string _toProject = "/projects";
		private string _toContracts = "/contracts";

	

		private ProjectModel _newProject = new();

		protected override async Task OnInitializedAsync()
		{			
			_newProject = new ProjectModel();
			_mainDepWorkers = await AuthorizedUserService.GetMainDepWorker();
			
			
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
			_contracts = await ContractForTableService.GetContractsAsync();

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
			_viewes = await BuildingViewService.GetAllBuildingViewAsync();

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
			_newProject.BuildingViewId = _selectedBuildingView.BuildingViewId > 0 ? _selectedBuildingView.BuildingViewId : null;
			_newProject.StageId = _selectedDesignStage.StageId > 0 ? _selectedDesignStage.StageId : null;
            _newProject.MainDepWorker = _selectedMainWorker.SurnameUser;

            var validationResult = _projectModelValidation.Validate(_newProject);

            if (validationResult.IsValid && ! await ProjectService.CheckBuildingNumber(_newProject.BuildingNumber))
            {
                if (await ProjectService.AddProjectAsync(_newProject))
                {
                    Snackbar.Add(Localizer["add"], Severity.Success);
                }
                else
                {
                    Snackbar.Add(Localizer["doNotAdd"], Severity.Error);
                }

                GoToPage(_toProject);
            }
            else
            {
                StringBuilder bld = new StringBuilder();
				if (await ProjectService.CheckBuildingNumber(_newProject.BuildingNumber))
				{
					bld.AppendLine($"Стройка с номер {_newProject.BuildingNumber} уже существует!");
				}
                foreach (var item in validationResult.Errors)
                {
                    bld.Append($"{item} ");
                }
                string errors = bld.ToString();
                Snackbar.Add($"{errors}", Severity.Error);
            }


		}
		private async Task Edit(int contractId)
		{
			if (contractId > 0)
			{
				await EntityManagementService.EditContractDialog(contractId);
				_contracts = await ContractForTableService.GetContractsAsync();
				_selectedContract = _contracts.FirstOrDefault(c => c.Contract.ContractID == contractId);

                StateHasChanged();

			}
			
		}

	}
}
