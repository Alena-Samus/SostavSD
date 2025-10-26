using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using SostavSD.Entities;
using SostavSD.Interfaces;
using SostavSD.Models;

namespace SostavSD.Pages.ProjectSostav.ProjectCard
{
    public partial class ChangeCiCDialog

    {
        [Inject] IStringLocalizer<ChangeCiCDialog> Localizer { get; set; }

        [CascadingParameter] MudDialogInstance ChangeProjectCiC{ get; set; }
        [Parameter] public string ProjectCiC { get; set; }
        private void Cancel()
        {
            ChangeProjectCiC.Close();
        }

        private void Submit()
        {

            ChangeProjectCiC.Close(DialogResult.Ok(ProjectCiC));

        }


    }
}
