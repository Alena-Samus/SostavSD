using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;

namespace SostavSD.Pages.ProjectSostav.ProjectCard
{
    public partial class ChangeCoefficientsDialog
    {
        [Inject] IStringLocalizer<ChangeCoefficientsDialog> Localizer { get; set; }

        [CascadingParameter] MudDialogInstance ChangeCoefficients { get; set; }
        [Parameter] public List<double?> PK { get; set; }


    
        private void Cancel()
        {
            ChangeCoefficients.Close();
        }

        private void Submit()
        {

            ChangeCoefficients.Close(DialogResult.Ok(PK));

        }

    }
}
