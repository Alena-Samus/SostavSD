using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SostavSD.Data;
using SostavSD.Entities;
using SostavSD.Interfaces;
using SostavSD.Models;
using IronXL;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;

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

		public async Task<byte[]> ExcelGenerate(List<CompanyModel> companies)
		{
			byte[] fileContents;
			WorkBook xlsxWorkbook = WorkBook.Create(ExcelFileFormat.XLSX);
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
				xlsxSheet[$"B{i + 2}"].Value = companies[i].CompanyDetails;
			}
			fileContents = xlsxWorkbook.ToByteArray();


			return fileContents;
		}

        public async Task<byte[]> WordGenerate(List<CompanyModel> companies)
        {
            byte[] fileContents;
            var stream = new MemoryStream();
            using (WordprocessingDocument doc = WordprocessingDocument.Create(stream, WordprocessingDocumentType.Document, true))
            {
                MainDocumentPart mainPart = doc.AddMainDocumentPart();

                new Document(new Body()).Save(mainPart);

                Body body = mainPart.Document.Body;

                body.Append(new Paragraph(new Run(new Text($"Список заказчиков"))));

                Table table = new Table();

                TableProperties tblProp = new TableProperties(
                    new TableBorders(
                        new TopBorder()
                        {
                            Val =
                            new EnumValue<BorderValues>(BorderValues.Seattle),
                            Size = 10
                        },
                        new BottomBorder()
                        {
                            Val =
                            new EnumValue<BorderValues>(BorderValues.Seattle),
                            Size = 10
                        },
                        new LeftBorder()
                        {
                            Val =
                            new EnumValue<BorderValues>(BorderValues.Seattle),
                            Size = 10
                        },
                        new RightBorder()
                        {
                            Val =
                            new EnumValue<BorderValues>(BorderValues.Seattle),
                            Size = 10
                        },
                        new InsideHorizontalBorder()
                        {
                            Val =
                            new EnumValue<BorderValues>(BorderValues.Seattle),
                            Size = 10
                        },
                        new InsideVerticalBorder()
                        {
                            Val =
                            new EnumValue<BorderValues>(BorderValues.Seattle),
                            Size = 10
                        }
                    )
                );
                table.AppendChild<TableProperties>(tblProp);

                foreach (var item in companies)
                {
                    TableRow tr = new TableRow();

                    TableCell tc1 = new TableCell();


                    tc1.Append(new TableCellProperties(
                        new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2400" }));

                    tc1.Append(new Paragraph(new Run(new Text(item.CompanyName))));

                    tr.Append(tc1);

                    TableCell tc2 = new TableCell();

                    tc2.Append(new Paragraph(new Run(new Text(item.CompanyDetails))));

                    tr.Append(tc2);

                    table.Append(tr);
                }

                doc.MainDocumentPart.Document.Body.Append(table);

                mainPart.Document.Save();
            }
            fileContents = stream.ToArray();

            return fileContents;
        }
    }
}
