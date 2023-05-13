namespace SostavSD.Entities
{
    public class BuildingView
    {
        public int BuildingViewId { get; set; }
        public string BuildingViewName { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}
