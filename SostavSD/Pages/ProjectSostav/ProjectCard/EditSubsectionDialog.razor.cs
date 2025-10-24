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
        [Inject] IStringLocalizer<EditSubsectionDialog> Localizer { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }

        [CascadingParameter] MudDialogInstance EditSubsection { get; set; }
        [Parameter] public SubsectionModel Subsection { get; set; }

        private List<SubsectionModel> subsectionsList = new List<SubsectionModel>();

        protected override async Task OnInitializedAsync()
        {
            subsectionsList.Add(Subsection);
        }



        private void Cancel()
        {
            EditSubsection.Close();
        }

        private void Submit()
        {
            EditSubsection.Close(DialogResult.Ok(Subsection));

        }

    }
}
