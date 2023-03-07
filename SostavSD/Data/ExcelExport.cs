using IronXL;
using Microsoft.JSInterop;
using SostavSD.Models;

namespace SostavSD.Data

{
	public class ExcelExport
	{
		public void ExcelGenerate(IJSRuntime iJSRuntime, List<CompanyModel> companies)
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
			iJSRuntime.InvokeAsync<ExcelExport>(
				"saveAsFile",
				"GeneratedExcel.xlsx",
				Convert.ToBase64String(fileContents)
			);
		}
	}
}
