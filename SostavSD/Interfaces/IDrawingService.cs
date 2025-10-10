using SostavSD.Models;

namespace SostavSD.Interfaces
{
    public interface IDrawingService
    {
        Task <List<DrawingModel>> GetDrawingModelsAsync ();
        Task<List<DrawingModel>> GetDrawingModelsByIdAsync(int id);
        void EditDrawing (DrawingModel currentDrawing);
    }
}
