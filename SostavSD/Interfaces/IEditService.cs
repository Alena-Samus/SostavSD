using SostavSD.Models;

namespace SostavSD.Interfaces
{
	public interface IEditService 
	{
		Task<bool> EditContractDialog (int contractId);

		Task<bool> EditProjectDialogAsync(ProjectModel newProject);

		Task <bool> EditDrawingDialogAsync (int drawingId);

        Task<bool> EditSubsectionDialogAsync(int subsectionId);

    }
}
