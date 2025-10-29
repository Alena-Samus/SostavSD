namespace SostavSD.Models
{
    public class SubsectionModel
    {
        public int SubsectionId { get; set; }
        public int? SerialNumber { get; set; }
        public string SubsectionName { get; set; }
        public int? ChapterId { get; set; }
        public ChapterModel Chapter { get; set; }
        public int? ProjectId { get; set; }
        public ProjectModel Project { get; set; }
        public string K1 { get; set; }
        public string K2 { get; set; }
        public string Norm { get; set; }
        public string Notes { get; set; }
    }
}
