using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using SostavSD.Interfaces;
using SostavSD.Models;

namespace SostavSD.Pages.ProjectSostav.ProjectCard
{
    public partial class EditSubsectionDialog
    {
        [Inject] IEntityManagementService EntityManagementService { get; set; }
        [Inject] IStringLocalizer<EditDrawingDialog> Localizer { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }

        [CascadingParameter] MudDialogInstance EditSubsection { get; set; }
        [Parameter] public SubsectionModel Subsection { get; set; }

        protected override async Task OnInitializedAsync()
        {

        }



        private void Cancel()
        {
            EditSubsection.Close();
            Snackbar.Add(Localizer["editingCanceled"], Severity.Info);

        }

        private void Submit()
        {

            EditSubsection.Close(DialogResult.Ok(Subsection));
            Snackbar.Add(Localizer["drawingEdited"], Severity.Success);

        }

    }
}
