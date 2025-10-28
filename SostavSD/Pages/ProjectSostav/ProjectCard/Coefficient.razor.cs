using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using SostavSD.Interfaces;
using SostavSD.Models;

namespace SostavSD.Pages.ProjectSostav.ProjectCard
{
    public partial class Coefficient
    {
        [Parameter] public List<CoefficientModel> ProjectCoefficiencets { get; set; }
        [Inject] IDialogService DialogService { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }
        [Inject] IStringLocalizer<Coefficient> Localizer { get; set; }
        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }

        
    }
}
