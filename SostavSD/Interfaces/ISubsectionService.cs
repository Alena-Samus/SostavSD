using SostavSD.Models;

namespace SostavSD.Interfaces
{
    public interface ISubsectionService
    {
        Task<List<SubsectionModel>> GetSubsectionByProjectIdAsync(int projectId);
        Task<bool> AddSubsectionsAsync(List<SubsectionModel> subsections);
        Task<bool> RemoveSubsectionsByProjectIdAsync(int projectId);
        Task<bool> RemoveSubsectionsByIdAsync(int subsectionId);
        Task<bool> EditSubsectionAsync(SubsectionModel subsection);
        Task <SubsectionModel> GetSubsectionById (int subsectionId);
    }
}
