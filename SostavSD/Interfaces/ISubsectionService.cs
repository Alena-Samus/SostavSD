using SostavSD.Models;

namespace SostavSD.Interfaces
{
    public interface ISubsectionService
    {
        Task<List<SubsectionModel>> GetSubsectionByProjectIdAsync(int projectId);
        Task<bool> AddSubsectionsAsync(List<SubsectionModel> subsections);
        Task<bool> RemoveSubsectionsAsync(int projectId);
    }
}
