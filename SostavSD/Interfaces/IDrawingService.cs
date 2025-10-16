using SostavSD.Models;

namespace SostavSD.Interfaces
{
    public interface IDrawingService
    {
        Task <List<DrawingModel>> GetDrawingModelsAsync ();
        Task<List<DrawingModel>> GetDrawingModelByIdAsync(int id);
        //Task EditDrawing (DrawingModel currentDrawing);
        Task <bool> AddDrawingsAsync (List<DrawingModel> drawingsList);
    }
}
