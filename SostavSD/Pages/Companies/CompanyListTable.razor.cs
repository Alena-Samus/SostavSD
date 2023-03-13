using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using SostavSD.Interfaces;
using SostavSD.Models;
using System.Net;
using System.Net.Mail;
using MimeKit;
using SostavSD.Data;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;

namespace SostavSD.Pages.Companies
{
    partial class CompanyListTable : ComponentBase
    {
        private List<CompanyModel> _companies = new List<CompanyModel>();

        private ICompanyService _companyService;
        private IDialogService _dialogService;
        private IJSRuntime _jsruntime;
        private IEmailService _emailService;

        private string searchString = "";

        private CompanyModel selectedItem = null;

        public CompanyListTable(ICompanyService companyService, IDialogService dialogService, IJSRuntime jsruntime, IEmailService emailService)
        {
            _companyService = companyService;
            _dialogService = dialogService;
            _jsruntime = jsruntime;
            _emailService = emailService;
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

		private async void ExportToExcel()
		{
            await _companyService.ExcelGenerate(_jsruntime, _companies);		
		}

        private void SendMail()

        {
            EmailMessage email= new EmailMessage();
            string attachmentsPath = @"Mail\test.txt";
            email.FromAddresses.Add(new EmailAddress { Name = "Sostav", Address = "sostavsd@mail.ru" });
            email.ToAddresses.Add(new EmailAddress { Name = "", Address = "sostavsd@gmail.com" });

  
            email.Subject = "Сообщение от MailKit";
            email.Content = "Сообщение от MailKit";

			_emailService.Send(email, attachmentsPath);                               

        }
	}
}
