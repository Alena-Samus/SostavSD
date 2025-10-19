using SostavSD.Models;

namespace SostavSD.Interfaces
{
	public interface IEntityManagementService : IBuildingViewService, IBuildingZoneService, IContractService, IContractForTableService, 
		IProjectService, IStatusService, IDesignStageService, ISourceOfFinancingService, IDrawingService, IDeppartService
	{
		Task<bool> EditContractDialog (int contractId);

		Task<bool> EditProjectAsync(ProjectModel newProject);

		Task <bool> EditDrawingDialogAsync (int drawingId);
        

    }
}
