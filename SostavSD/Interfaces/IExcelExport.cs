using SostavSD.Models;

namespace SostavSD.Interfaces
{
    public interface IExcelExport
    {
        Task<byte[]> ExcelGenerate(List<CompanyModel> companies);
    }
}
