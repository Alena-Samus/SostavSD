using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using SostavSD.Data;
using SostavSD.Entities;
using SostavSD.Interfaces;
using SostavSD.Models;
using IronXL;


namespace SostavSD.Services
{
    public class CompanyService : ICompanyService

    {
        private readonly SostavSDContext _context;
        private readonly IMapper _mapper;
       


        public CompanyService(SostavSDContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task AddCompany(CompanyModel newCompany)
        {
            Company _newCompany = _mapper.Map<Company>(newCompany);
            _context.company.Add(_newCompany);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCompany(int companyId)
        {
            var companyToRemove = await _context.company.FindAsync(companyId);

            if (companyToRemove != null)
            {
                _context.company.Remove(companyToRemove);
                _context.SaveChanges();

            }
        }

        public async Task EditCompany(CompanyModel currentCompany)
        {
            Company companytAfterEdit = _mapper.Map<Company>(currentCompany);
            _context.company.Update(companytAfterEdit);
            await _context.SaveChangesAsync();
        }



		public async Task<List<CompanyModel>> GetAllCompany()
        {
            var companyList = _context.company
                .AsNoTracking();

            return _mapper.Map<List<CompanyModel>>(await companyList.ToListAsync());
        }

        public async Task<CompanyModel> GetSingleCompany(int companytId)
        {
            var singleCompany = await _context.company.FirstOrDefaultAsync(e => e.CompanyID == companytId);

            if (singleCompany != null)
            {
                _context.company.Entry(singleCompany).State = EntityState.Detached;
            }
            return _mapper.Map<CompanyModel>(singleCompany);
        }

		public async Task ExcelGenerate(IJSRuntime iJSRuntime, List<CompanyModel> companies)
		{
			byte[] fileContents;
			WorkBook xlsxWorkbook = WorkBook.Create(IronXL.ExcelFileFormat.XLSX);
			xlsxWorkbook.Metadata.Author = "IronXL";
			//Add a blank WorkSheet
			WorkSheet xlsxSheet = xlsxWorkbook.CreateWorkSheet("new_sheet");
			//Add data and styles to the new worksheet
			xlsxSheet["A1"].Value = "Company Name";
			xlsxSheet["B1"].Value = "Company Details";

			xlsxSheet["A1:B1"].Style.Font.Bold = true;
			for (int i = 0; i < companies.Count; i++)
			{
				xlsxSheet[$"A{i + 2}"].Value = companies[i].CompanyName;
			}
			for (int i = 0; i < companies.Count; i++)
			{
				xlsxSheet[$"B{i + 2}"].Value = companies[i].CompanyName;
			}
			fileContents = xlsxWorkbook.ToByteArray();
			await iJSRuntime.InvokeAsync<CompanyModel>(
				"saveAsFile",
				"GeneratedExcel.xlsx",
				Convert.ToBase64String(fileContents)
			);
		}


	}
}
