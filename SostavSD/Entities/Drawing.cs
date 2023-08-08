namespace SostavSD.Entities
{
    public class Drawing
    {
        public int DrawingId { get; set; }
        public string DrawingName { get; set; }
        public int ProjectId { get; set; }

        public Project Project { get; set; }
        public int? StatusId { get; set; }
        public Status Status { get; set; }
        public int? DrawingPriority { get; set; }
        public DateTime? DrawingReleaseDateBySchedule { get; set; }
        public DateTime? DrawingReleaseDateDepertment { get; set; }
        public DateTime? DrawingDateOfAdmissionToDepartmetn { get; set; }
        public string Group { get; set; }

        public ICollection<Estimate> Estimates { get; set; }
        public ICollection<DrawingEstimate> DrawingsEstimates { get; set; }
    }
}
