using Microsoft.AspNetCore.Components;
using MudBlazor;
using SostavSD.Models;

namespace SostavSD.Pages.Companies
{
    partial class CompanyDialogAddOrEdit : ComponentBase
    {
        [CascadingParameter] MudDialogInstance AddOrEditCompany { get; set; }
        [Parameter] public CompanyModel Company { get; set; }

        private void Cancel()
        {
            AddOrEditCompany.Cancel();

        }

        private void Submit()
        {
            Console.WriteLine("111111111");
            AddOrEditCompany.Close(DialogResult.Ok(Company));
        }
    }
}
