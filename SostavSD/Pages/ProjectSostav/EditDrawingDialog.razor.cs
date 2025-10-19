using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using SostavSD.Classes.Validation;
using SostavSD.Entities;
using SostavSD.Interfaces;
using SostavSD.Models;
using SostavSD.Services;

namespace SostavSD.Pages.ProjectSostav
{
    public partial class EditDrawingDialog: ComponentBase
    {
        [Inject] IEntityManagementService EntityManagementService { get; set; }
        [Inject] IStringLocalizer<DrawingsWithoutEstimates> Localizer { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }

        [CascadingParameter] MudDialogInstance EditDrawing { get; set; }
        [Parameter] public DrawingModel Drawing { get; set; }

        protected override async Task OnInitializedAsync()
        {
        }
        private void Cancel()
        {
            EditDrawing.Cancel();

        }

        private void Submit()
        {
            //var validationResult = _contractModelValidation.Validate(Contract);
            //string errors = string.Empty;
            //if (validationResult.IsValid)
            //{
                EditDrawing.Close(DialogResult.Ok(Drawing));
                Snackbar.Add("Done", Severity.Success);
            //}
            //else
            //{
            //    foreach (var item in validationResult.Errors)
            //    {
            //        errors += $"{item} ";
            //    }
            //    Snackbar.Add($"{errors}", Severity.Error);
            //}

        }
    }
}
