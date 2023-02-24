using MudBlazor;
using SostavSD.Data.Interfaces;
using SostavSD.Data.Services;
using SostavSD.Models;


namespace SostavSD.Pages.Companies
{
    partial class CompanyListTable
    {
        private List<CompanyModel> _companies = new List<CompanyModel>();

        private ICompanyService _companyService;

        private string searchString = "";

        private CompanyModel selectedItem = null;

        private List<CompanyModel> selectedItems = new List<CompanyModel>();

        private IEnumerable<CompanyModel> contracts = new List<CompanyModel>();

        public CompanyListTable(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        protected override async Task OnInitializedAsync()
        {
            await GetCompanies();
        }

        private async Task<List<CompanyModel>> GetCompanies()
        {
            _companies = await _companyService.GetAllCompany();
            return _companies;
        }
        private bool FilterFuncCurrent(CompanyModel company) => FilterFunc(company, searchString);

        private bool FilterFunc(CompanyModel company, string searchString)
        {
            bool result = string.IsNullOrWhiteSpace(searchString) || (company.CompanyName.Contains(searchString, StringComparison.OrdinalIgnoreCase)) ||
                 (company.CompanyDetails.Contains(searchString, StringComparison.OrdinalIgnoreCase));

            return result;
        }

        private async Task Delete(int companyId)
        {
            CompanyModel currentCompanyName = await _companyService.GetSingleCompany(companyId);
            bool? result = await _dialogService.ShowMessageBox(
                "Удаление договора",
               $"Удалить договор \"{currentCompanyName.CompanyName}\"?",
                yesText: "Удалить", cancelText: "Отмена");


            if (result ?? false)
            {
                await _companyService.DeleteCompany(companyId);
                await GetCompanies();
            }

        }

        private async Task Edit(int companyId)
        {
            var parameters = new DialogParameters();
            var companyToEdit = await _companyService.GetSingleCompany(companyId);
            parameters.Add("Company", companyToEdit);
            var dialog = await _dialogService.Show<CompanyDialogAddOrEdit>("uodate", parameters).Result;
            
            if (dialog != null)
            {
                CompanyModel newContract = (CompanyModel)dialog.Data;
                await _companyService.EditCompany(companyToEdit);
                await GetCompanies();
            }
            
        }


        private async Task CreateNewCompany()
        {

            var parameters = new DialogParameters();
            parameters.Add("Company", new CompanyModel());


            var dialog = await _dialogService.Show<CompanyDialogAddOrEdit>("Добавить новую компанию", parameters).Result;

            if (dialog.Data != null)
            {
                CompanyModel newCompany = (CompanyModel)dialog.Data;
                await _companyService.AddCompany(newCompany);
                await GetCompanies();
            }
        }
    }
}
