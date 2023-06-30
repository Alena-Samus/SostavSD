namespace SostavSD.Entities
{
    public class ConstructionPhase
    {
        public int PhaseId { get; set; }
        public string PhaseName { get; set; }

        public ICollection<Project> Projects { get; set; }
    }
}
