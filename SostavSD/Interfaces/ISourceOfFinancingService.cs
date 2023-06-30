using SostavSD.Models;

namespace SostavSD.Interfaces
{
    public interface ISourceOfFinancingService
    {
        Task<List<SourceOfFinacingModel>> GetSourcesOfFinancingModelAsync();
    }
}
