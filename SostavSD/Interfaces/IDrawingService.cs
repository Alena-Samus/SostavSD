using SostavSD.Models;

namespace SostavSD.Interfaces
{
    public interface IDrawingService
    {
        Task <List<DrawingModel>> GetDrawingModelsAsync ();
        Task <List<DrawingModel>> GetDrawingModelsByProjectIdAsync(int id);
        Task<List<DrawingModel>> GetDrawingModelsByGroupIdAsync(int groupId);
        Task<DrawingModel> GetSingleDrawingById(int drawingId);
        Task <int> AddSingleDrawingAsync (DrawingModel drawingModel);
        Task <bool> EditDrawingAsync(DrawingModel currentDrawing);
        Task<bool> RemoveDrawingsAsync (int id);
    }
}
