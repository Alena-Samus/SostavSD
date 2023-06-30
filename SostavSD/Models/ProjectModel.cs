using SostavSD.Entities;

namespace SostavSD.Models
{
	public class ProjectModel
	{
		public int ProjectId { get; set; }
		public string ProjectName { get; set; }
		public string BuildingNumber { get; set; }
		public int? Priority { get; set; }
		public int ContractId { get; set; }
		public ContractModel Contract { get; set; }
		public string ConstructionPhase { get; set; }
		public int? StageId { get; set; }
		public DesignStageModel DesignStage { get; set; }
		public int? BuildingViewId { get; set; }
		public BuildingViewModel BuildingView { get; set; }
		public int? StatusId { get; set; }
		public StatusModel Status { get; set; }
		public DateTime? StatusDate { get; set; }
		public DateTime? ProjectReleaseDate { get; set; }
		public DateTime? ProjectReleaseDateByContract { get; set; }
		public DateTime? WorkStartDate { get; set; }
		public DateTime? PriceLevel { get; set; }
		public string PrintType { get; set; }
		public string CiCVersion { get; set; }
	}
}
