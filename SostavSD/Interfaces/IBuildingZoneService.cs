using SostavSD.Models;

namespace SostavSD.Interfaces
{
    public interface IBuildingZoneService
    {
       Task <List<BuildingZoneModel>> GetBuildingZoneModelsAsync ();
        Task <BuildingZoneModel> GetBuildingZoneByIdAsync (int zoneId);
    }
}
