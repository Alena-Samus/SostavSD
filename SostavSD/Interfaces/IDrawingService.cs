using SostavSD.Models;

namespace SostavSD.Interfaces
{
    public interface IDrawingService
    {
        Task <List<DrawingModel>> GetDrawingModelsAsync ();
        Task<List<DrawingModel>> GetDrawingModelsByIdAsync(int id);
        //Task EditDrawing (DrawingModel currentDrawing);
        Task AddDrawings (List<DrawingModel> drawingsList);
    }
}
