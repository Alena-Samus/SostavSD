using SostavSD.Models;

namespace SostavSD.Interfaces
{
    public interface IBuildingZoneService
    {
       Task <List<BuildingZoneModel>> GetBuildingZoneModelsAsync ();
       BuildingZoneModel GetBuildingZoneById (int zoneId);
    }
}
