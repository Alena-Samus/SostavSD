using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using SostavSD.Interfaces;
using SostavSD.Models;
using SostavSD.Pages.Projects;

namespace SostavSD.Pages.ProjectSostav.ProjectCard
{
    public partial class CopyChaptersDialog
    {
        [Inject] IEntityManagementService EntityManagementService { get; set; }
        [Inject] IStringLocalizer<CopyChaptersDialog> Localizer { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }

        [CascadingParameter] MudDialogInstance CopyChapters { get; set; }
        [Parameter] public string BuildingNumber { get; set; }

        DefaultFocus DefaultFocus { get; set; } = DefaultFocus.FirstChild;
        protected override async Task OnInitializedAsync()
        {

        }


        private void Cancel()
        {
            CopyChapters.Close();
            Snackbar.Add(Localizer["copyingCanceled"], Severity.Info);

        }

        private void Submit()
        {
            if (EntityManagementService.CheckBuildingNumber(BuildingNumber))
            {
                CopyChapters.Close(DialogResult.Ok(BuildingNumber));
            }
            else
            {
                Snackbar.Add(Localizer["badBuildingNumber"], Severity.Error);
            } 
        }

    }

}
