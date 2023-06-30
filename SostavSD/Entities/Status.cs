namespace SostavSD.Entities
{
    public class Status
    {
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public bool IsProject { get; set; }
        public bool IsDrawing { get; set; }
        public bool IsEstimate { get; set; }

        public ICollection<Project> Projects { get; set; }

    }
}
