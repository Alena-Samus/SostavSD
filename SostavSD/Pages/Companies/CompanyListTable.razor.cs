using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using SostavSD.Interfaces;
using SostavSD.Models;
using System.Net;
using System.Net.Mail;
using MimeKit;

namespace SostavSD.Pages.Companies
{
    partial class CompanyListTable : ComponentBase
    {
        private List<CompanyModel> _companies = new List<CompanyModel>();

        private ICompanyService _companyService;
        private IDialogService _dialogService;
        private IJSRuntime _jsruntime;

        private string searchString = "";

        private CompanyModel selectedItem = null;

        public CompanyListTable(ICompanyService companyService, IDialogService dialogService, IJSRuntime jsruntime)
        {
            _companyService = companyService;
            _dialogService = dialogService;
            _jsruntime = jsruntime;
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

        private async void SendMail()
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("Sostav", "sostavsd@mail.ru")); //отправитель сообщения
            message.To.Add(new MailboxAddress("", "sostavsd@gmail.com")); //адресат сообщения
            message.Subject = "Сообщение от MailKit"; //тема сообщения
            message.Body = new BodyBuilder() { HtmlBody = "<div style=\"color: green;\">Сообщение от MailKit</div>" }.ToMessageBody(); //тело сообщения (так же в формате HTML)
            

            using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect("smtp.mail.ru", 465, true); //либо использум порт 465
                client.Authenticate("sostavsd@mail.ru", "xqeXY7cmcM3zDtVp8LGP"); //логин-пароль от аккаунта
                client.Send(message);

                client.Disconnect(true);

            }

        }
	}
}
