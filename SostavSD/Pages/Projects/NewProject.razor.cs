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
		
		[Inject] IStringLocalizer<NewProject> Localizer { get; set; }
		[Inject] ISnackbar Snackbar { get; set; }
		[Inject] IEntityManagementService EntityManagementService { get; set; }

	

		private List<ContractForTableModel> _contracts = new();
		private List<BuildingViewModel> _viewes = new();
		private List<DesignStageModel> _stages = new();
		

		private ContractForTableModel _selectedContract = new();
		
		private BuildingViewModel _selectedBuildingView = new();

		private DesignStageModel _selectedDesignStage = new();

        private ProjectModelValidation _projectModelValidation = new();


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
			_contracts = await EntityManagementService.GetContractsAsync();

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
			_viewes = EntityManagementService.GetAllBuildingView();

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
			_stages = await EntityManagementService.GetAllDesignStageAsync();

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

            var validationResult = _projectModelValidation.Validate(_newProject);

            if (validationResult.IsValid && !EntityManagementService.CheckBuildingNumber(_newProject.BuildingNumber))
            {
                if (await EntityManagementService.AddProjectAsync(_newProject))
                {
                    Snackbar.Add("Add", Severity.Success);
                }
                else
                {
                    Snackbar.Add("Don't add", Severity.Error);
                }

                GoToPage(_toProject);
            }
            else
            {
                StringBuilder bld = new StringBuilder();
				if (EntityManagementService.CheckBuildingNumber(_newProject.BuildingNumber))
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
				_contracts = await EntityManagementService.GetContractsAsync();
				_selectedContract = _contracts.FirstOrDefault(c => c.Contract.ContractID == contractId);

                StateHasChanged();

			}
			
		}

	}
}
