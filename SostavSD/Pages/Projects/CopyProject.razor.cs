using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using SostavSD.Interfaces;

namespace SostavSD.Pages.Projects
{
	partial class CopyProject
	{
		[CascadingParameter] MudDialogInstance NewProject { get; set; }
		[Inject] public IStringLocalizer<CopyProject> Localizer { get; set; }
		[Inject] public IProjectService ProjectService { get; set; }
		[Inject] ISnackbar Snackbar { get; set; }
		[Parameter] public string BuildingNumber { get; set; }
		

		private void Cancel()
		{
			NewProject.Cancel();
		}

		private async Task Submit()
		{
            if (! await ProjectService.CheckBuildingNumber(BuildingNumber))
			{
				NewProject.Close(DialogResult.Ok(BuildingNumber));
			}
			else
			{
				Snackbar.Add(Localizer["badBuildingNumber"], Severity.Error);
			}
			
		}
	}
}
