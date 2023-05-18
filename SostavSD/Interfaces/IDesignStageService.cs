using SostavSD.Models;

namespace SostavSD.Interfaces
{
	public interface IDesignStageService
	{
		Task <List<DesignStageModel>> GetAllDesignStageAsync ();
	}
}
