using SostavSD.Models;

namespace SostavSD.Interfaces
{
    public interface ISubsectionService
    {
        Task<List<SubsectionModel>> GetSubsectionByProjectIdAsync(int projectId);
    }
}
