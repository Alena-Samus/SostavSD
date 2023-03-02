using Microsoft.AspNetCore.Components;
using MudBlazor;
using SostavSD.Interfaces;
using SostavSD.Models;

namespace SostavSD.Pages.Contracts
{
    public partial class ContractAddNewAndEdit : ComponentBase
    {
        [CascadingParameter] MudDialogInstance AddOrEditContract { get; set; }
        
        [Parameter] public ContractModel Contract { get; set; }
        private ICompanyService _companyService;
        private List<CompanyModel> _companies = new List<CompanyModel>();
        

        public ContractAddNewAndEdit(ICompanyService companyService)
        {
            _companyService= companyService;
        }
        protected override async Task OnInitializedAsync()
        {
            _companies = await _companyService.GetAllCompany();
        }
        private void Cancel()
        {
            AddOrEditContract.Cancel();
           
        }

        private void Submit()
        {
            AddOrEditContract.Close(DialogResult.Ok(Contract));
        }
    }
}
