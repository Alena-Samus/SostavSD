namespace SostavSD.Entities
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string BuildingNumber { get; set; }
        public int? Priority { get; set; }
        public int ContractId { get; set; }
        public Contract Contract { get; set; }
        public string ConstructionPhase { get; set; }
        public int? StageId { get; set; }
        public DesignStage DesignStage { get; set; }
        public int? BuildingViewId { get; set; }
        public BuildingView BuildingView { get; set; }
        public int? StatusId { get; set; }
        public Status Status { get; set; }
        public DateTime? StatusDate { get; set; }
        public DateTime? ProjectReleaseDate { get; set; }
        public DateTime? ProjectReleaseDateByContract { get;set; }
        public DateTime? WorkStartDate { get; set; }
        public DateTime? PriceLevel { get; set; }
        public string PrintType { get; set; }
        public string CiCVersion { get; set; }

        public ICollection<Drawing> Drawings { get; set; }
    }
}
