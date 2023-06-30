namespace SostavSD.Entities
{
    public class DesignStage
    {
        public int StageId { get; set; }
        public string StageName { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}
