using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using SostavSD.Interfaces;
using SostavSD.Models;

namespace SostavSD.Pages.ProjectSostav.ProjectCard
{
    public partial class Coefficient
    {
        [Inject] IProjectService ProjectService { get; set; }
        [Inject] IDialogService DialogService { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }
        [Inject] IStringLocalizer<Coefficient> Localizer { get; set; }

        [Parameter] public List<CoefficientModel> ProjectCoefficiencets { get; set; }
        [Parameter] public double? OHROPR1 { get; set; }
        [Parameter] public double? OHROPR2 { get; set; }
        [Parameter] public int ProjectId { get; set; }
        [Parameter] public EventCallback<ProjectCard> UpdateCoefficients {  get; set; }
        private List<double?> Coefficients = new List<double?>();


        protected override async Task OnInitializedAsync()
        {

        }

        private async Task Edit ()
        {
            Coefficients.Add(OHROPR1);
            Coefficients.Add(OHROPR2);
            var parameters = new DialogParameters();

            parameters.Add("PK", Coefficients);
            var dialog = await DialogService.Show<ChangeCoefficientsDialog>("Edit", parameters).Result;
            if (dialog.Data != null)
            {
                
                if (await ProjectService.UpdateCoefficientsAsync(ProjectId, Coefficients[0], Coefficients[1]))
                {
                    Snackbar.Add(@Localizer["coefficientsChanged"], Severity.Success);
                    await UpdateCoefficients.InvokeAsync();
                }
                else
                {
                    Snackbar.Add(@Localizer["coefficientsNotChanged"], Severity.Error);
                }
            }
            
        }


    }
}
