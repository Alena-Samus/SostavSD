using SostavSD.Models;

namespace SostavSD.Data.Interfaces
{
    public interface ICompanyService
    {
        Task<List<CompanyModel>> GetAllCompany();
        Task DeleteCompany(int companytId);
        Task AddCompany(CompanyModel newCompany);

        Task<CompanyModel> GetSingleCompany(int companytId);

        Task EditCompany(CompanyModel currentCompany);
    }
}
