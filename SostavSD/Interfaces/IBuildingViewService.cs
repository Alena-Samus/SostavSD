using SostavSD.Models;

namespace SostavSD.Interfaces
{
	public interface IBuildingViewService
	{
		Task<List<BuildingViewModel>> GetAllBuildingView();
	}
}
