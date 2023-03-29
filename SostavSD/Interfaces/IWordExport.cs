using SostavSD.Models;

namespace SostavSD.Interfaces
{
    public interface IWordExport
    {
        Task<byte[]> WordGenerate(List<CompanyModel> companies);
    }
}
