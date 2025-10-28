using SostavSD.Models;

namespace SostavSD.Interfaces
{
    public interface ICoefficientService
    {
        Task <List<CoefficientModel>> GetCoefficiensByBuildingViewIdBuildingZoneId (int buildingViewId, int buildingZoneId);
    }
}
