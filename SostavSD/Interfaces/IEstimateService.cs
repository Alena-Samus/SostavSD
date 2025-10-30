using SostavSD.Models;

namespace SostavSD.Interfaces
{
    public interface IEstimateService
    {
        Task<int> AddEstimateAsync(EstimateModel estimate);
    }
}
