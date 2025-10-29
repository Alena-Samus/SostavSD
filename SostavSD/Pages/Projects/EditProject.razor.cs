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
        [Inject] IAuthorizedUserService AuthorizedUserService { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }

        private ContractModel _selectedContract = new();
        private ProjectModelValidation _projectModelValidation = new();
        private UsersForListModel _selectedWorker = new();
        private DesignStageModel _selectedStage = new();
        private BuildingViewModel _selectedView = new();

        private List<StatusModel> _statuses = new();
        private List<DesignStageModel> _stages = new();
        private List<BuildingViewModel> _views = new();
        private List<ContractModel> _contracts = new();
        private List<UsersForListModel> _mainWorkers = new();

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
            _mainWorkers = await AuthorizedUserService.GetMainDepWorker();
            _selectedWorker = _mainWorkers.FirstOrDefault(p => p.SurnameUser == Project.MainDepWorker);
            _selectedStage = _stages.FirstOrDefault(p => p.StageId == Project.StageId);
            _selectedView = _views.FirstOrDefault(p => p.BuildingViewId == Project.BuildingViewId);
            
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

                Project.StatusId = elem.Value;
                Project.StatusDate = DateTime.Now;

        }

        private void ChangeStage(DesignStageModel selectedStage)
        {
            _selectedStage = selectedStage;
            Project.StageId = selectedStage.StageId;

        }
        private void ChangeView(BuildingViewModel selectedView)
        {

            Project.BuildingViewId = selectedView.BuildingViewId;
            _selectedView = selectedView;

        }

        private void ChangeWorker(UsersForListModel selectedWorker)
        {
            Project.MainDepWorker = selectedWorker.SurnameUser;
            _selectedWorker = selectedWorker;
        }

    }
}
