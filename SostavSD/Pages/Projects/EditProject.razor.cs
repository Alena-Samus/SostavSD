using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using SostavSD.Classes.Validation;
using SostavSD.Interfaces;
using SostavSD.Models;
using SostavSD.Services;
using System.Text;

namespace SostavSD.Pages.Projects
{
	partial class EditProject
	{
		[CascadingParameter] MudDialogInstance EditCurrentProject { get; set; }

		[Parameter] public ProjectModel Project { get; set; }
		[Inject] IStringLocalizer<EditProject> Localizer { get; set; }
		[Inject] IEntityManagementService EntityManagementService { get; set; }

        [Inject] ISnackbar Snackbar { get; set; }

        private ContractModel _selectedContract = new();
        private ProjectModelValidation _projectModelValidation = new();

        List<StatusModel> _statuses = new();
        List<DesignStageModel> _stages = new();
        List<BuildingViewModel> _views = new();
        List<ContractModel> _contracts = new();
        


        protected override async Task OnInitializedAsync()
		{
            _selectedContract = Project.Contract;
            await GetLists();

        }
		private void Cancel()
		{
			EditCurrentProject.Cancel();
		}

		private void Submit()
		{
            var validationResult = _projectModelValidation.Validate(Project);
            
            if (validationResult.IsValid)
            {
                Project.ContractId = _selectedContract.ContractID;
                EditCurrentProject.Close(DialogResult.Ok(Project));
            }
            else
            {
                StringBuilder bld = new StringBuilder();
                foreach (var item in validationResult.Errors)
                {
                    bld.Append($"{item} ");
                }
                string errors = bld.ToString();
                Snackbar.Add($"{errors}", Severity.Error);
            }

        }

		private async Task GetLists()
		{
            var _statusesForTable = await EntityManagementService.GetAllStatusAsync();
            _statuses = _statusesForTable.Where(x => x.IsProject).ToList();
            _stages = await EntityManagementService.GetAllDesignStageAsync();
            _views = EntityManagementService.GetAllBuildingView();
            
        }
		public async Task Edit(int contractId)
		{
			await EntityManagementService.EditContractDialog(contractId);
		}
        protected async Task<IEnumerable<ContractModel>> FindContract(string value)
        {
            _contracts = await EntityManagementService.GetAllContract();

            if (string.IsNullOrEmpty(value))
            {               
                return _contracts;
            }
            else
            {               
                return _contracts.Where(x => x.Index.Contains(value, StringComparison.InvariantCultureIgnoreCase));
            }

        }

        private void ChangeDate(int? elem)
        {
            if (Project.StatusId != elem)
            {
                Project.StatusId= elem;
                Project.StatusDate = DateTime.Now;
            }            
        }

    }
}
