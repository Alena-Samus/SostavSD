using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using SostavSD.Interfaces;
using SostavSD.Models;
using SostavSD.Classes.Email;

using System.Drawing.Text;
using MimeKit.Encodings;

namespace SostavSD.Pages.Companies
{
    partial class CompanyListTable : ComponentBase
    {
        private List<CompanyModel> _companies = new List<CompanyModel>();

        private ICompanyService _companyService;
        private IDialogService _dialogService;
        private IJSRuntime _jsruntime;
        private IEmailService _emailService;
        private IWordExport _wordExport;
        private IPdfExport _pdfExport;
        private IExcelExport _excelExport;

        private string _email;
        private string _user;

		private string searchString = "";

        private CompanyModel selectedItem = null;
        

        public CompanyListTable(ICompanyService companyService, IDialogService dialogService, IJSRuntime jsruntime, IEmailService emailService, 
            IWordExport wordExport, IPdfExport pdfExport, IExcelExport excelExport)
        {
            _companyService = companyService;
            _dialogService = dialogService;
            _jsruntime = jsruntime;
            _emailService = emailService;
            _wordExport = wordExport;
            _pdfExport = pdfExport;
            _excelExport = excelExport;
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
            var dialog = await _dialogService.Show<CompanyDialogAddOrEdit>("update", parameters).Result;
            
            if (dialog != null)
            {
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
           byte[] currentXLS = await _excelExport.ExcelGenerate(_companies);

			await _jsruntime.InvokeAsync<CompanyModel>(
				"saveAsFile",
				"GeneratedExcel.xlsx",
				Convert.ToBase64String(currentXLS)
			);
		}
        private async void ExportToWord()
        {
            byte[] currentDOCX = await _wordExport.WordGenerate(_companies);
            await _jsruntime.InvokeAsync<CompanyModel>(
                "saveAsFile",
                "GeneratedWord.docx",
                Convert.ToBase64String(currentDOCX)
            );
        }
        private async void ExportToPDF()
        {
            byte[] currentPDF = await _pdfExport.PDFGenerate(_companies);    

            await _jsruntime.InvokeAsync<CompanyModel>(
                "saveAsFile",
                "GeneratedPDF.pdf",
                Convert.ToBase64String(currentPDF)
            );
        }
        
        
        private async void SendMail()

        {
			string text = "Email Test From Company Page"; 

			
			using (FileStream fileStream = File.Open(@"Mail\test.txt", FileMode.Create))
			{
				using (StreamWriter output = new StreamWriter(fileStream))
				{					
					output.Write(text);
				}
			}
		
		    EmailMessage email = new EmailMessage();
			_email = await _emailService.GetEmail();
			_user = await _emailService.GetEmail();
			email.FromAddress = new EmailAddress();
            email.ToAddress = new EmailAddress { Name = "", Address = _email };


            email.Subject = "Send Test Email From Company Page";
            email.Content = $"Hello, {_user}, Email Test From Company Page";
            email.Attachment = @"Mail\test.txt";

            _emailService.Send(email);
            showMailAlert = true;
            File.Delete(@"Mail\test.txt");
        }

		private bool showMailAlert = false;

		private void CloseMe(bool value)
		{
			showMailAlert = !value;

		}
	}
}
