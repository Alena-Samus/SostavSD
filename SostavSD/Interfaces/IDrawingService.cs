using SostavSD.Models;

namespace SostavSD.Interfaces
{
    public interface IDrawingService
    {
        Task <List<DrawingModel>> GetDrawingModelsAsync ();
    }
}
