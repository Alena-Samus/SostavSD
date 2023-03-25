using SostavSD.Models;

namespace SostavSD.Interfaces
{
    public interface IPdfExport
    {
        Task<byte[]> PDFGenerate(List<CompanyModel> companies);
    }
}
