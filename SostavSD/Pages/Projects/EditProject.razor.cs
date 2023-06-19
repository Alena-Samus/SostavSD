using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using SostavSD.Interfaces;
using SostavSD.Models;
using SostavSD.Services;

namespace SostavSD.Pages.Projects
{
	partial class EditProject
	{
		[CascadingParameter] MudDialogInstance EditCurrentProject { get; set; }

		[Parameter] public ProjectModel Project { get; set; }
		[Inject] IStringLocalizer<EditProject> Localizer { get; set; }
		[Inject] IEntityManagementService EntityManagementService { get; set; }
				
        List<StatusModel> _statuses = new();
        List<DesignStageModel> _stages = new();
        List<BuildingViewModel> _views = new();


        protected override async Task OnInitializedAsync()
		{
            await GetLists();

        }
		private void Cancel()
		{
			EditCurrentProject.Cancel();
		}

		private void Submit()
		{
            EditCurrentProject.Close(DialogResult.Ok(Project));		
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

	}
}
