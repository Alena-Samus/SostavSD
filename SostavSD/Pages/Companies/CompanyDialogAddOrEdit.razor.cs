using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using SostavSD.Classes.Validation;
using SostavSD.Models;

namespace SostavSD.Pages.Companies
{
    partial class CompanyDialogAddOrEdit : ComponentBase
    {
        [CascadingParameter] MudDialogInstance AddOrEditCompany { get; set; }
        [Parameter] public CompanyModel Company { get; set; }

        [Inject] ISnackbar Snackbar { get; set; }
        [Inject] IStringLocalizer<CompanyDialogAddOrEdit> localizer { get; set; }


		private CompanyModelValidator _companyModelValidator = new CompanyModelValidator();
        private void Cancel()
        {
            AddOrEditCompany.Cancel();

        }

        private void Submit()
        {
            var validationResult = _companyModelValidator.Validate(Company);

            string errors = string.Empty;

            if (validationResult.IsValid)
            {
				AddOrEditCompany.Close(DialogResult.Ok(Company));
				Snackbar.Add("Company added",Severity.Success);
			}
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    errors += $"{item} ";
                }
                Snackbar.Add($"{errors}",Severity.Error);
            }
            
        }
    }
}
