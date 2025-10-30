using SostavSD.Models;

namespace SostavSD.Interfaces
{
    public interface IDrawingEstimateService
    {
        Task<bool> AddDrawingEstimate(DrawingEstimateModel drawingEstimate);
    }
}
