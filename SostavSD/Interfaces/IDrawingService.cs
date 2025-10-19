using SostavSD.Models;

namespace SostavSD.Interfaces
{
    public interface IDrawingService
    {
        Task <List<DrawingModel>> GetDrawingModelsAsync ();
        Task <List<DrawingModel>> GetDrawingModelByProjectIdAsync(int id);
        Task<DrawingModel> GetSingleDrawingById(int drawingId);
        Task <bool> EditDrawingAsync(DrawingModel currentDrawing);
        Task<bool> AddDrawingsAsync (List<DrawingModel> drawingsList);
        Task<bool> RemoveDrawingsAsync (int id);
    }
}
