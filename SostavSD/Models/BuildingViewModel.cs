namespace SostavSD.Models
{
	public class BuildingViewModel
	{
		public int BuildingViewId { get; set; }
		public string BuildingViewName { get; set; }
		public ICollection<ProjectModel> Projects { get; set; }
	}
}
