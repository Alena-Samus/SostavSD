using Microsoft.AspNetCore.Components;
using MudBlazor;
using SostavSD.Models;

namespace SostavSD.Pages.Companies
{
    partial class CompanyDialogAddOrEdit
    {
        [CascadingParameter] MudDialogInstance AddOrEditCompany { get; set; }
        [Parameter] public CompanyModel Company { get; set; }

        private void Cancel()
        {
            AddOrEditCompany.Cancel();

        }

        private void Submit()
        {
            AddOrEditCompany.Close(DialogResult.Ok(Company));
        }
    }
}
