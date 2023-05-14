namespace SostavSD.Models
{
	public class DesignStageModel
	{
		public int StageId { get; set; }
		public string StageName { get; set; }
		public ICollection<ProjectModel> Projects { get; set; }
	}
}
